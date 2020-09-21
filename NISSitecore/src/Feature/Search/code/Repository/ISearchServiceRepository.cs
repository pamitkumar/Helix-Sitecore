using NISSitecore.Feature.Search.Models;
using NISSitecore.Foundation.Search.Services;
using Sitecore.Data.Items;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Search.Repository
{
    /// <summary>
    /// Searches the search index for content.
    /// </summary>
    public interface ISearchServiceRepository
    {
        /// <summary>
        /// Search for content using an instance of <see cref="IDBSolrQuery"/>.
        /// </summary>
        SolrQueryResults<PageContentResultItem> Search(
            string q,
            Item solrQueryItem,
            int startRow = 0,
            string language = "",
            string facetValue = "");

        /// <summary>
        /// Search for content using a Sitecore <see cref="Item"/>.
        /// </summary>
        SolrQueryResults<PageContentResultItem> Search(
            string q,
            IDBSolrQuery query,
            int startRow = 0,
            string language = "",
            string facetValue = "");

        /// <summary>
        /// Builds a <see cref="IDBSolrQuery"/> from a Sitecore <see cref="Item"/>.
        /// </summary>
        IDBSolrQuery BuildIDBSolrQueryFromItem(Item solrQueryItem);
    }
}
