using NISSitecore.Foundation.Sitemap.Models;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace NISSitecore.Foundation.Sitemap.Pipelines.HTTPRequestBegin
{
    public class Sitemap : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (!System.IO.File.Exists(args.HttpContext.Request.PhysicalPath) &&
                 !System.IO.Directory.Exists(args.HttpContext.Request.PhysicalPath))
            {
                Assert.ArgumentNotNull(args, "args");
                //Check if the request is of sitemap.xml then only allow the request to serve sitemap.xml
                if (args.Url == null || !args.Url.FilePath.ToLower().EndsWith("sitemap.xml")) return;
                try
                {
                    // Homepage of the Website.
                    // Start path will give homepage including Multisite.
                    var homepage = Context.Database.GetItem(args.StartPath);
                    

                    var urlSet = new Urlset();
                    urlSet.Url = new Url[] { new Url(), new Url() };
                    Type[] types = new Type[] { typeof(Url) };

                    //Create node of Homepage in Sitemap.
                    var tmpurlset = new List<object>();
                    var config = AppendLanguage();

                    if (!ExcludeItemFromSitemap(homepage))
                    {
                        tmpurlset.Add(new Url
                        {
                            Loc = GetAbsoluteLink(LinkManager.GetItemUrl(homepage, new ItemUrlBuilderOptions() { LanguageEmbedding = (config == 2 ? LanguageEmbedding.Always : (config == 1 ? LanguageEmbedding.AsNeeded : LanguageEmbedding.Never)) })),
                            Lastmod = homepage.Statistics.Updated.ToString("yyyy-MM-dd hh:mm:ss")
                        });
                    }

                    // Get all decendants of Homepage to create full Sitemap.
                    var childrens = homepage.Axes.GetDescendants();
                    //Remove the items whose templateid is in exclude list
                    var finalcollection = childrens.Where(x => !ExcludeItemFromSitemap(x)).ToList();

                    tmpurlset.AddRange(finalcollection.Select(childItem => new Url
                    {
                        Loc = GetAbsoluteLink(LinkManager.GetItemUrl(childItem, new ItemUrlBuilderOptions() { LanguageEmbedding = (config == 2 ? LanguageEmbedding.Always : (config == 1 ? LanguageEmbedding.AsNeeded : LanguageEmbedding.Never)) })),
                        Lastmod = childItem.Statistics.Updated.ToString("yyyy-MM-dd hh:mm:ss")
                    }));

                    // Populate created collection to right object
                    urlSet.Url = tmpurlset.ToArray();
                    var ser = new XmlSerializer(typeof(Urlset),types);
                    //Write XML Response for Sitemap.
                    var response = HttpContext.Current.Response;
                    response.AddHeader("Content-Type", "text/xml");
                    ser.Serialize(response.OutputStream, urlSet);
                    HttpContext.Current.Response.End();
                    //Response Ends Here
                }
                catch (Exception ex)
                {
                    Log.Error("Error - Sitemap.xml.", ex, this);
                }
            }
        }

        ///

        /// Crete Absolute url as per the site
        /// 

        ///
        ///
        private static string GetAbsoluteLink(string relativeUrl)
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + relativeUrl;
        }

        ///

        /// Append language or not in URL to return language specific sitemap.xml
        /// 

        ///
        private static int AppendLanguage()
        {
            return string.IsNullOrEmpty(Sitecore.Configuration.Settings.GetSetting("LanguageEmbedForSitemap")) ? 0 : System.Convert.ToInt32((Sitecore.Configuration.Settings.GetSetting("LanguageEmbedForSitemap")));
        }

        ///

        /// This method will get a list of excluding template ids and will check if the passed item is in
        /// 

        ///
        ///
        private static bool ExcludeItemFromSitemap(Item objItem)
        {
            //Check if the item is having any version
            if (objItem.Versions.Count > 0)
            {
                var excludeItems = Sitecore.Configuration.Settings.GetSetting("ExcludeSitecoreItemsByTemplatesInSitemap");
                var collection = excludeItems.Split(',').ToList();
                return collection.Contains(objItem.TemplateID.ToString());
            }
            return true;
        }
    }
}