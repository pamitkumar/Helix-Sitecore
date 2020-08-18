using NISSitecore.Feature.Maps.Models;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore;
using Sitecore.ContentSearch.SearchTypes;
using NISSitecore.Feature.Maps.Models.Search;
using Sitecore.Common;
using Sitecore.ContentSearch.Linq;
using Glass.Mapper.Sc;
using Sitecore.Data;

namespace NISSitecore.Feature.Maps.Repositories
{
    public class MapPointRepository : IMapPointRepository
    {
        public IEnumerable<IMapPoint> GetAll(Item contextItem/*,ID startPath*/)
        {
            using (var context = ContentSearchManager.CreateSearchContext(new SitecoreIndexableItem(contextItem)))
            {
                var query = context.GetQueryable<MapPointSearchItem>();
                query = query.Where(i => i.ItemId == contextItem.ID);
                //query = query.Where(i => i.Paths.Contains(startPath));
                var results = query.GetResults();
                if (results != null && results.TotalSearchResults > 0)
                {
                    var data = results.Hits.Where(i => i.Document != null).Select(i => i.Document);
                    var service = new SitecoreService(Context.Database); //to use with Glass
                    var resultSet = new List<IMapPoint>();

                    foreach (var item in data)
                    {
                        IMapPoint resultItem = service.GetItem<IMapPoint>(item.GetItem());

                        if (resultItem != null)
                        {
                            resultSet.Add(resultItem);
                        }
                    }
                    return resultSet;
                }
            }

            return null;            
        }
    }
}