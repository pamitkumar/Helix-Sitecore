
namespace Corlate.Feature.PageContent.Models
{
    using System.Collections.Generic;

    public class TagsOrCategoriesSidebarFilter
    {
        public string SectionTitle { get; set; }

        public bool DisplayItemsAsTags { get; set; }

        public string BlogArchivePageURL { get; set; }

        public List<BlogCategory> BlogCategories { get; set; }

        public List<BlogTag> BlogTags { get; set; }
    }
}