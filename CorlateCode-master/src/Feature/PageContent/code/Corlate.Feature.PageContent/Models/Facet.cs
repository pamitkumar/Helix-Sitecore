
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    
    public class Facet : CustomItem
    {
        public Facet(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.Facet.Fields.Title.ToString();
            }
        }

        public string DescriptionID
        {
            get
            {
                return References.Templates.Facet.Fields.Description.ToString();
            }
        }

        public string ImageCSSClass
        {
            get
            {
                return InnerItem.Fields[References.Templates.Facet.Fields.ImageCSSClass].Value;
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.Facet.Fields.IsActive].Value == "1";
            }
        }
    }

}
