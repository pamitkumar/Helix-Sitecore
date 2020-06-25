using NISSitecore.Feature.Navigation.Extension;
using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Navigation.Pipeline
{
    public class SeoProcessor : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            HttpContext context = HttpContext.Current;

            if (context == null)
            {
                return;
            }

            string requestUrl = context.Request.Url.ToString();

            if (string.IsNullOrEmpty(requestUrl))
            {
                return;
            }

            if (requestUrl.ToLower().TrimEnd('/').EndsWith("robots.txt"))
            {
                ProcessRobots(context);
            }
            else if (requestUrl.ToLower().EndsWith("sitemap-xml"))
            {
                ProcessSitemap(context);
            }
        }

        private static void ProcessRobots(HttpContext context)
        {
            //the default robots, may be overide by the value of the "Site Robots TXT" field in the "Site Config" item
            string robotsTxtContent = @"User-agent: *" + Environment.NewLine +
                                      "Disallow: /Lightbox/" + Environment.NewLine +
                                      "Disallow: /lightbox/" + Environment.NewLine + Environment.NewLine +
                                      "Sitemap: " + HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host + "/sitemap-xml";

            if (global::Sitecore.Context.Site != null && global::Sitecore.Context.Database != null)
            {
                Item siteconfigNode = GetConfigNode();

                if (siteconfigNode != null)
                {
                    var configRobotTxt = siteconfigNode.GetStringValue("Site Robots TXT");
                    if (!string.IsNullOrEmpty(configRobotTxt))
                    {
                        robotsTxtContent = configRobotTxt;
                        if (!robotsTxtContent.ToLower().Contains("sitemap:") || !robotsTxtContent.ToLower().Contains("/sitemap-xml"))
                        {
                            robotsTxtContent += Environment.NewLine + Environment.NewLine + "Sitemap: " + HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host + "/sitemap-xml";
                        }
                    }
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(robotsTxtContent);
            context.Response.End();
        }

        private static void ProcessSitemap(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            context.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine);
            context.Response.Write("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">" + Environment.NewLine);

            //some site specific logic for multisite
            if (System.String.CompareOrdinal(Sitecore.Context.Site.Name, "myBelgiumWebsite") == 0)
            {
                var language = new List<string>();
                language.Add("nl-BE");
                language.Add("fr-BE");
                context.Response.Write(XMLSitemap.GetCacheXml(language));
            }
            else
            {
                context.Response.Write(XMLSitemap.GetCacheXml(null));
            }

            context.Response.Write(Environment.NewLine + "</urlset>");
            context.Response.End();
        }

        public static Item GetConfigNode()
        {
            Sitecore.Sites.SiteContext site = Sitecore.Context.Site;
            if (site == null) return null;

            Sitecore.Data.Database db = Sitecore.Context.Database;
            if (db == null) return null;
            Item start = db.GetItem(site.StartPath);
            if (start == null) return null;

            var siteConfigNode = start.Parent.Children.FirstOrDefault(item => item.TemplateName == "Site Config");
            return siteConfigNode;
        }
    }
}