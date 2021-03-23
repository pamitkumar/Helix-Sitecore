
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;

    public class BannerCarouselSlide : CustomItem
    {
        public BannerCarouselSlide(Item innerItem) : base(innerItem) { }

        public string ImageID
        {
            get
            {
                return References.Templates.BannerCarouselSlide.Fields.Image.ToString();
            }
        }

        public string TitleID
        {
            get
            {
                return References.Templates.BannerCarouselSlide.Fields.Title.ToString();
            }
        }

       public string DescriptionID
        {
            get
            {
                return References.Templates.BannerCarouselSlide.Fields.Description.ToString();
            }
        }

        public string CTAID
        {
            get
            {
                return References.Templates.BannerCarouselSlide.Fields.CTA.ToString();
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.BannerCarouselSlide.Fields.IsActive].Value == "1";
            }
        }
    }

}
