
namespace Corlate.Feature.PageContent.Models
{
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    public class PopularPostsSource : CustomItem
    {
        public PopularPostsSource(Item innerItem) : base(innerItem) { }

        public string TitleID
        {
            get
            {
                return References.Templates.PopularPostsSource.Fields.Title.ToString();
            }
        }

        public List<Blog> FeaturedBlogs { get; set; }
        

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.PopularPostsSource.Fields.IsActive].Value == "1";
            }
        }
    }

}
