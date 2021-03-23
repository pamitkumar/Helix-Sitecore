
namespace Corlate.Feature.PageContent.Models
{
    using Corlate.Feature.PageContent;
    using Corlate.Foundation.Common.Utilities;
    using Sitecore.Data.Items;

    public class FooterSocialLink : CustomItem
    {
        public FooterSocialLink(Item innerItem) : base(innerItem) { }

        public string Title
        {
            get
            {
                return InnerItem.Fields[References.Templates.FooterSocialLink.Fields.Title].Value;
            }
        }

        public string TargetURL
        {
            get
            {
                return SitecoreUtility.GetLinkURL(InnerItem, References.Templates.FooterSocialLink.Fields.TargetURL);
            }
        }

        public string CSSClass
        {
            get
            {
                return InnerItem.Fields[References.Templates.FooterSocialLink.Fields.CSSClass].Value;
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.FooterSocialLink.Fields.IsActive].Value == "1";
            }
        }
    }

}
