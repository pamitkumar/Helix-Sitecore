using NISSitecore.Feature.Search.Models;
using NISSitecore.Foundation.Search.Services;
using Sitecore.Data.Items;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Search.Repository
{
    public class SearchServiceRepository : IDBSearchBase, ISearchServiceRepository
    {
        /// <summary>
        /// Search for content using an instance of <see cref="IDBSolrQuery"/>.
        /// </summary>
        public SolrQueryResults<PageContentResultItem> Search(
            string q,
            Item solrQueryItem,
            int startRow = 0,
            string language = "",
            string facetValue = "")
        {
            // TODO: custom business logic here.

            IDBSolrQuery query = BuildIDBSolrQueryFromItem(solrQueryItem);

            return Search(q, query, startRow, language, facetValue);
        }

        /// <summary>
        /// Search for content using a Sitecore <see cref="Item"/>.
        /// </summary>
        public SolrQueryResults<PageContentResultItem> Search(
            string q,
            IDBSolrQuery query,
            int startRow = 0,
            string language = "",
            string facetValue = "")
        {
            // TODO: custom business logic here

            // Filter by facet.
            var filters = new List<ISolrQuery>();

            if (!string.IsNullOrWhiteSpace(query.FacetField) && !string.IsNullOrWhiteSpace(facetValue))
            {
                filters.Add(new SolrQueryByField(query.FacetField, facetValue));
            }

            return IDBSearch<PageContentResultItem>(q, query, startRow, language, filters);
        }

        /// <summary>
        /// Builds a <see cref="IDBSolrQuery"/> from a Sitecore <see cref="Item"/>.
        /// </summary>
        public IDBSolrQuery BuildIDBSolrQueryFromItem(Item solrQueryItem)
        {
            return base.BuildIDBSolrQuery(solrQueryItem);
        }
    }
}