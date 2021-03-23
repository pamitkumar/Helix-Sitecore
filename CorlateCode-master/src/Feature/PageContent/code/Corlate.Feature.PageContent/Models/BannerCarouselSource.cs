
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using System.Collections.Generic;
   
    public class BannerCarouselSource : CustomItem
    {
        public BannerCarouselSource(Item innerItem) : base(innerItem) { }

        public List<BannerCarouselSlide> Slides { get; set; }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.BannerCarouselSource.Fields.IsActive].Value == "1";
            }
        }
    }

}
