
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;

    public class BlogCategory : CustomItem
    {
        public BlogCategory(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.BlogCategory.Fields.Title.ToString();
            }
        }

        public string Title
        {
            get
            {
                return InnerItem.Fields[References.Templates.BlogCategory.Fields.Title].Value;
            }
        }

        public string IsActiveID
        {
            get
            {
                return References.Templates.BlogCategory.Fields.IsActive.ToString();
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.BlogCategory.Fields.IsActive].Value == "1";
            }
        }
    }

}
