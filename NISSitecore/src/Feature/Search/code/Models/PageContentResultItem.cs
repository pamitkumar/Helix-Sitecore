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

        [IndexField("customcontent")]
        public virtual string CustomContent { get; set; }

        [IndexField("includeinsearchresults_b")]
        public virtual bool IncludeInSearchResults { get; set; }
    }
}