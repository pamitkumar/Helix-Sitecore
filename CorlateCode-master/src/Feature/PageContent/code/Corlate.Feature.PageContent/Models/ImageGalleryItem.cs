
namespace Corlate.Feature.PageContent.Models
{
    using Corlate.Feature.PageContent;
    using Corlate.Foundation.Common.Utilities;
    using Sitecore.Data.Items;

    public class ImageGalleryItem : CustomItem
    {
        public ImageGalleryItem(Item innerItem) : base(innerItem) { }

        public string ThumbnailURL
        {
            get
            {
                return SitecoreUtility.GetMediaItemURL(InnerItem, References.Templates.ImageGalleryItem.Fields.Thumbnail);
            }
        }

        public string ImageURL
        {
            get
            {
                return SitecoreUtility.GetMediaItemURL(InnerItem, References.Templates.ImageGalleryItem.Fields.Image);
            }
        }

        public string Title
        {
            get
            {
                return InnerItem.Fields[References.Templates.ImageGalleryItem.Fields.Title].Value;
            }
        }

        public string ViewLabelID
        {
            get
            {
                return References.Templates.ImageGalleryItem.Fields.ViewLabel.ToString();
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.ImageGalleryItem.Fields.IsActive].Value == "1";
            }
        }
    }    

}
