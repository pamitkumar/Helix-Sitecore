
namespace Corlate.Feature.PageContent.Models
{
    using System.Collections.Generic;

    public class BlogArchive
    {
        public string BlogArchivePageItemID { get; set; }

        public string SelectedCategoryID { get; set; }

        public string SelectedTagID { get; set; }

        public List<Blog> BlogTeasers { get; set; }

        public Pagination Pagination { get; set; }
    }
}