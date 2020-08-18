using NISSitecore.Feature.Maps.Models;
using NISSitecore.Feature.Maps.Models.Search;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Maps.Repositories
{
    public interface IMapPointRepository
    {
        IEnumerable<IMapPoint> GetAll(Item contextItem);
    }
}
