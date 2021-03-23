
namespace Corlate.Feature.Navigation.Models
{
    using Corlate.Feature.Navigation;
    using Corlate.Foundation.Common.Utilities;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    public class NavigationItem : CustomItem
    {
        public NavigationItem(Item innerItem) : base(innerItem) { }

        public string TargetURLID
        {
            get
            {
                return References.Templates.NavigationItem.Fields.TargetURL.ToString();
            }
        }

        public string TargetURL
        {
            get
            {
                return SitecoreUtility.GetLinkURL(InnerItem, References.Templates.NavigationItem.Fields.TargetURL);
            }
        }

        public string TargetURLDescription
        {
            get
            {
                return SitecoreUtility.GetLinkDescription(InnerItem, References.Templates.NavigationItem.Fields.TargetURL);
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.NavigationItem.Fields.IsActive].Value == "1";
            }
        }

        public List<NavigationItem> SubmenuItems { get; set; }

        /// <summary>
        /// Checks if the menu item in top menu is for the current page
        /// </summary>
        public bool IsCurrentPage
        {
            get
            {
                bool isCurrentPage = false;

                LinkField linkField = InnerItem.Fields[TargetURLID.ToString()];

                if (linkField != null && linkField.LinkType.ToLower() == "internal")
                {
                    isCurrentPage = linkField.TargetItem != null && linkField.TargetItem.Paths.ContentPath == Sitecore.Context.Item.Paths.ContentPath;
                }

                return isCurrentPage;
            }
        }
    }    

}
