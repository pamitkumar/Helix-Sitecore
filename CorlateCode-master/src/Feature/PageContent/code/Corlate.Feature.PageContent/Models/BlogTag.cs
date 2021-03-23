
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;

    public class BlogTag : CustomItem
    {
        public BlogTag(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.BlogTag.Fields.Title.ToString();
            }
        }

        public string Title
        {
            get
            {
                return InnerItem.Fields[References.Templates.BlogTag.Fields.Title].Value;
            }
        }

        public string IsActiveID
        {
            get
            {
                return References.Templates.BlogTag.Fields.IsActive.ToString();
            }
        }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.BlogTag.Fields.IsActive].Value == "1";
            }
        }
    }

}
