
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using System.Collections.Generic;


    public class ContactUsFormSource : CustomItem
    {
        public ContactUsFormSource(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.ContactUsFormSource.Fields.Title.ToString();
            }
        }

        public string DescriptionID
        {
            get
            {
                return References.Templates.ContactUsFormSource.Fields.Description.ToString();
            }
        }

        public string AddressID
        {
            get
            {
                return References.Templates.ContactUsFormSource.Fields.Address.ToString();
            }
        }

        public List<FooterSocialLink> SocialNetworkLinks { get; set; }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.ContactUsFormSource.Fields.IsActive].Value == "1";
            }
        }
    }

}
