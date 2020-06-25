using Microsoft.Ajax.Utilities;
using NISSitecore.Feature.Navigation.Extension;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using static NISSitecore.Feature.Navigation.Constants;

namespace NISSitecore.Feature.Navigation.Repositories
{
    public static class HTMLSitemapRepository
    {
        private static Func<string, string, string> NodeWithMarkupHomepage = (displayText, itemURL) =>
           string.Format("<p><a href='{0}'>{1}</a></p>", itemURL, displayText);
       
        private static Func<string, string, string> NodeWithMarkup = (displayText, itemURL) =>
           string.Format("<div><a href='{0}'>{1}</a></div>", itemURL, displayText);

        private static Func<CheckboxField, bool> ShowNode = (showInHTMLSiteMap) => (showInHTMLSiteMap != null) ? showInHTMLSiteMap.Checked : false;

        public static Func<CheckboxField, bool> ShowNode1 { get => ShowNode; set => ShowNode = value; }

        public static string BuildSitemapHTML()
        {
            var homeitem = Sitecore.Context.Item.GetHomeItem();
            //var detailList = homeitem.Axes.GetDescendants().ToList();
            //detailList.Add(homeitem);

            var options = global::Sitecore.Links.LinkManager.GetDefaultUrlBuilderOptions();
            options.AlwaysIncludeServerUrl = true;

            return CreateSiteMapUrls(homeitem, options);
        }


        private static string CreateSiteMapUrls(Item homeItem,ItemUrlBuilderOptions urlOptions)
        {
            StringBuilder returnString = new StringBuilder();
                       
            ////Sitecore Fields each page must contain this field.
            var HideInHtmlSitemap = "Hide in HTML Sitemap";

            if (!homeItem.GetCheckBoxValueDefaultTrue(HideInHtmlSitemap))                
            {
                returnString.AppendFormat(NodeWithMarkupHomepage(GetTitle(homeItem), LinkManager.GetItemUrl(homeItem, urlOptions)));
                returnString.Append(GetSiteMapTree(homeItem));
            }

                
            //foreach (Item item in detailList)
            //{
            //    var url = LinkManager.GetItemUrl(item, urlOptions);

            //    var title = GetTitle(item);

            //    returnString.Append(NodeWithMarkup(GetTitle(item), url));
            //}
            return returnString.ToString();
        }


        private static string GetSiteMapTree(Item node)
        {
            if (node == null)
            {
                return "";
            }
            var HideInHtmlSitemap = "Hide in HTML Sitemap";
            var options = global::Sitecore.Links.LinkManager.GetDefaultUrlBuilderOptions();
            options.AlwaysIncludeServerUrl = true;
            IEnumerable<Item> children = node.Children.Where(x => !string.IsNullOrEmpty(x.Name));
            StringBuilder sb = new StringBuilder("");

            if (children == null || !children.Any())
            {
                return "";
            }            

            foreach (var child in children)
            {
                if (!child.GetCheckBoxValueDefaultTrue(HideInHtmlSitemap))
                {
                    sb.Append(NodeWithMarkup(GetTitle(child), Sitecore.Links.LinkManager.GetItemUrl(child, options)));
                }

                var contentChildren = child.Axes.GetDescendants().ToList();
                if (contentChildren.Any())
                {
                    sb.Append(GetSiteMapTree(child));
                }
            }           

            return sb.ToString();
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

        private static string GetTitle(Item node)
        {
            string displayText = string.Empty;

            if (node.Fields[SitemapContent.Fields.Title] != null)
            {
                displayText = node.Fields[SitemapContent.Fields.Title].Value;
            }

            if (string.IsNullOrEmpty(displayText) && node.Name != null)
            {
                displayText = (!string.IsNullOrEmpty(node.DisplayName)) ? node.DisplayName : node.Name;
            }

            return displayText;
        }
    }
}