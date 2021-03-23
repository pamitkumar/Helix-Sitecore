
namespace Corlate.Feature.PageContent.Models
{
    using Foundation.Common.Utilities;
    using Sitecore.Data.Items;

    public class Banner : CustomItem
    {
        public Banner(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.Banner.Fields.Title.ToString();
            }
        }              

        public string ImageURL
        {
            get
            {
                return SitecoreUtility.GetMediaItemURL(InnerItem, References.Templates.Banner.Fields.Image);
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.Banner.Fields.IsActive].Value == "1";
            }
        }
    }
}
