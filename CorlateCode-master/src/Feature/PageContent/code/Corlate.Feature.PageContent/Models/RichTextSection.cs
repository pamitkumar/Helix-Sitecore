
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
   
    public class RichTextSection : CustomItem
    {
        public RichTextSection(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.RichTextSection.Fields.Title.ToString();
            }
        }

        public string DescriptionID
        {
            get
            {
                return References.Templates.RichTextSection.Fields.Description.ToString();
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.RichTextSection.Fields.IsActive].Value == "1";
            }
        }
    }

}
