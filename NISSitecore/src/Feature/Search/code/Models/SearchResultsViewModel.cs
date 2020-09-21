using PagedList;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Search.Models
{
    public class SearchResultsViewModel
    {
        public string Query { get; set; }

        public int PageNumber { get; set; }

        public string Facet { get; set; }

        public string FacetName { get; set; }

        public SolrQueryResults<PageContentResultItem> SearchResults { get; set; }

        public StaticPagedList<PageContentResultItem> PagedResults { get; set; }

        public FacetCount[] FacetCounts { get; set; }

        [DebuggerDisplay("Key= {Key}, Name= {Name}, Count= {Count}")]
        public class FacetCount
        {
            public string Key { get; set; }

            public string Name { get; set; }

            public int Count { get; set; }
        }
    }
}