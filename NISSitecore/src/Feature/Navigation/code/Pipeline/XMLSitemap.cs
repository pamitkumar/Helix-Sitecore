using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NISSitecore.Feature.Navigation.Extension;
using Sitecore;
using Sitecore.Caching;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;

namespace NISSitecore.Feature.Navigation.Pipeline
{
    public static class XMLSitemap
    {
        static readonly Cache sitemapCache = new Sitecore.Caching.Cache("idbsitecore-xml-sitemap", StringUtil.ParseSizeString("2MB"));
             
        static XMLSitemap()
        {
            //flush the cache after a publish.
            Sitecore.Events.Event.Subscribe("publish:end", SitemapCache_OnPublishEnd);
            Sitecore.Events.Event.Subscribe("publish:end:remote", SitemapCache_OnPublishEnd);
        }

        static void SitemapCache_OnPublishEnd(object sender, System.EventArgs eventArgs)
        {
            sitemapCache.Clear();
        }

        /// <summary>
        /// Get the sitemap cache or generate if the cache is empty
        /// </summary>
        /// <param name="languagelist">leave blank for monolingual</param>
        /// <returns></returns>
        public static string GetCacheXml(List<string> languagelist)
        {
            if (sitemapCache != null)
            {
                string protocol = HttpContext.Current.Request.IsSecureConnection ? "-s-" : "-";
                //key contain the host and also the database because the difference between master and web.
                var cachekey = Context.Site.Name + protocol + HttpContext.Current.Request.Url.Host.ToLower() + "-" + Sitecore.Context.Database.Name;
                if (languagelist != null)
                {
                    cachekey += "-" + string.Concat(languagelist);
                }
                string sitemap = (string)sitemapCache.GetValue(cachekey);
                if (string.IsNullOrEmpty(sitemap))
                {
                    if (languagelist != null && languagelist.Count > 0)
                    {
                        sitemap = GetXml(languagelist);
                    }
                    else
                    {
                        sitemap = GetXml();
                    }
                    sitemapCache.Add(cachekey, sitemap);
                }
                return sitemap;
            }
            return string.Empty;
        }

        //single language
        private static string GetXml()
        {
            var homeitem = Sitecore.Context.Item.GetHomeItem();
            var detailList = homeitem.Axes.GetDescendants().ToList();
            detailList.Add(homeitem);

            var options = global::Sitecore.Links.LinkManager.GetDefaultUrlBuilderOptions();
            options.AlwaysIncludeServerUrl = true;
            return CreateSiteMapUrls(detailList, options);
        }

        //multi language
        private static string GetXml(List<string> languagelist)
        {
            Database db = global::Sitecore.Context.Database;
            string sitemapLinks = string.Empty;
            foreach (var language in languagelist)
            {
                Language currentSiteLanugage;
                if (Language.TryParse(language, out currentSiteLanugage))
                {
                    Sitecore.Context.SetLanguage(currentSiteLanugage, true);
                }
                var homeitem = global::Sitecore.Context.Item.GetHomeItem();
                var detailList = homeitem.Axes.GetDescendants().ToList();
                detailList.Add(homeitem);
                var options = global::Sitecore.Links.LinkManager.GetDefaultUrlBuilderOptions();
                options.AlwaysIncludeServerUrl = true;
                options.LanguageEmbedding = LanguageEmbedding.Always;
                options.Language = Language.Parse(language);
                options.EmbedLanguage(LanguageManager.GetLanguage(language));
                sitemapLinks += CreateSiteMapUrls(detailList, options);
            }
            return sitemapLinks;
        }

        private static string CreateSiteMapUrls(List<Item> detailList, ItemUrlBuilderOptions urlOptions)
        {
            StringBuilder returnString = new StringBuilder();

            const string defaultpagechange = "daily";

            //Sitecore Fields eache page must contain this field.
            //var HideInSeoXmlSitemap = "Hide in SEO XML Sitemap";
            var HideInSeoXmlSitemap = "Hide in SEO XML Sitemap";
            var XmlSitemapPriority = "XML Sitemap Priority";
            var XmlSitemapChangeFreq = "XML Sitemap Change Frequency";

            foreach (Item item in detailList)
            {
                if (!item.GetCheckBoxValueDefaultTrue(HideInSeoXmlSitemap))
                {
                    if (item.GetDropLinkValue("Meta Robots", "Meta Content").ToUpper().StartsWith("INDEX"))
                    {
                        var url = LinkManager.GetItemUrl(item, urlOptions);
                        if (url.ToLower().Contains("/custombla/"))
                        {
                            //optional some custom logic to hide your site specific pages.
                            continue;
                        }
                        var prio = item.GetStringValue(XmlSitemapPriority);
                        var changefreq = item.GetStringValue(XmlSitemapChangeFreq);
                        if (string.IsNullOrEmpty(changefreq))
                        {
                            changefreq = defaultpagechange;
                        }
                        if (string.IsNullOrEmpty(prio))
                        {
                            returnString.AppendFormat("<url><loc>{0}</loc><changefreq>{1}</changefreq></url>", url, changefreq);
                        }
                        else
                        {
                            returnString.AppendFormat("<url><loc>{0}</loc><changefreq>{1}</changefreq><priority>{2}</priority></url>\n", url, changefreq, prio);
                        }
                    }
                }
            }
            return returnString.ToString();
        }

        public static Item GetHomeItem(this Item item)
        {
            global::Sitecore.Sites.SiteContext site = global::Sitecore.Context.Site;

            if (site == null)
            {
                return null;
            }

            global::Sitecore.Data.Database db = global::Sitecore.Context.Database;
            return db.GetItem(site.StartPath);
        }
    }
}