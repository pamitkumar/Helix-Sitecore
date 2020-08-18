using NISSitecore.Foundation.ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace NISSitecore.Feature.Maps.Models
{
    [SitecoreType]
    public interface IMapPoint:IGlassBase
    {
        [SitecoreField(FieldName = "MapPointName")]       
        new string Name { get; set; }

        [SitecoreField(FieldName= "MapPointAddress")] 
        string Address { get; set; }

        [SitecoreField(FieldName = "MapPointLocation")]
        string Location { get; set; }
    }
}
