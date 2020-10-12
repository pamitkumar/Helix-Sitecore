using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using NISSitecore.Foundation.SitecoreExtensions.Extensions;

namespace NISSitecore.Foundation.Search.Infrastructure.Fields
{
    public class ContentField : AbstractComputedIndexField
    {
        //this content item id is base page item id.(only sitecore item which has presentation values will be added as base page item.having this item in inheritance will be detected as Page item which can be linked or shown in search results page)
        private const string ContentBaseItemId = "{8D507DEE-B1AB-45B3-AE10-7C3AE938E3CF}";
        private const string SearchResults = "{CB496AB1-3F7E-4339-9E8E-49D82A25E380}";
        public override object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            if (item == null) return null;
            //if (!IsValidItem(item)) return null;
            StringBuilder sbData = new StringBuilder();
            var renderings = GetRenderingReferences(item, "default");
            if (renderings == null)
                return null;
            if (item.Versions.IsLatestVersion())
            {
                //the fields of the current item will be added to index
                foreach (Field field in item.Fields)
                {
                    if (!string.IsNullOrWhiteSpace(field.Value))
                    {
                        sbData.AppendFormat("{0} ", StripHtml(field.Value));
                    }
                }
                foreach (var rendering in renderings)
                {
                    if (string.IsNullOrEmpty(rendering.Settings.DataSource)) continue;
                    var dataSourceItem = item.Database.GetItem(rendering.Settings.DataSource);
                    //the fields of the rendering of the current item will be added to index
                    if (dataSourceItem != null)
                    {
                        foreach (Field field in dataSourceItem.Fields)
                        {
                            if (!string.IsNullOrWhiteSpace(field.Value))
                            {
                                sbData.AppendFormat("{0} ", StripHtml(field.Value));
                            }
                        }
                    }
                }
            }
            return sbData.ToString().Trim();
        }
        private Sitecore.Layouts.RenderingReference[] GetRenderingReferences(Item item, string deviceName)
        {
            LayoutField layoutField = item.Fields["__final renderings"];
            if (layoutField == null)
                return null;
            Sitecore.Layouts.RenderingReference[] renderings = null;
            if (item.Database != null)
            {
                renderings = layoutField.GetReferences(GetDeviceItem(item.Database, deviceName));
            }
            else
            {
                renderings = layoutField.GetReferences(GetDeviceItem(Sitecore.Context.Database, deviceName));
            }
            return renderings;
        }
        private bool IsValidItem(Item item)
        {
            Item searchPage = Sitecore.Configuration.Factory.GetDatabase("web").GetItem(new ID(SearchResults));
            bool isValid = true;
            MultilistField multilistField = searchPage.Fields["Searchable Page Templates"];
            if (multilistField != null)
                isValid = multilistField.TargetIDs.Contains(item.TemplateID) ? true : false;
           
            if (!item.IsDerived(new ID(ContentBaseItemId)) || !item.Paths.FullPath.ToLowerInvariant().Contains("/sitecore/content/")) return false;
            return isValid;
        }
        private DeviceItem GetDeviceItem(Sitecore.Data.Database db, string deviceName)
        {
            return db.Resources.Devices.GetAll().Where(d => d.Name.ToLower() == deviceName.ToLower()).First();
        }
        public static string StripHtml(string source)
        {
            var htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
            var removedTags = htmlRegex.Replace(source, string.Empty);
            return HttpUtility.HtmlDecode(removedTags);
        }
    }
}