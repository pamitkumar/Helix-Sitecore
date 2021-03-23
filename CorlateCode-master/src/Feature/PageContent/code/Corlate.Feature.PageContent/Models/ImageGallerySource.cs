
namespace Corlate.Feature.PageContent.Models
{
    using Corlate.Feature.PageContent;
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    public class ImageGallerySource : CustomItem
    {
        public ImageGallerySource(Item innerItem) : base(innerItem) { }

        public List<ImageGalleryItem> GalleryItems { get; set; }              

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.ImageGallerySource.Fields.IsActive].Value == "1";
            }
        }
    }    

}
