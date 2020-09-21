using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Search.Models
{
    public class PageContentResultItem: SitecoreUISearchResultItem
    {
        [IndexField("title_t")]
        public virtual string Title { get; set; }

        [IndexField("summary_t")]
        public virtual string Description { get; set; }

        //[IndexField("summary_t")]
        //public virtual string ArticleType { get; set; }

        //[IndexField("display_date_tdt")]
        //public virtual DateTime DisplayDate { get; set; }
    }
}