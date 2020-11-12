using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace NISSitecore.Feature.SocialShare.Models
{
    public class SocialItemProperty
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public Item SocialItem { get; set; }

        public string Img { get; set; }

        public CheckboxField IsHideButtonChecked { get; set; }
    }
}