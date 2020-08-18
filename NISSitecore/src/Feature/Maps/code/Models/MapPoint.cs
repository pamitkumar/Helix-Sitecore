using Sitecore.ContentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Maps.Models
{
    public class MapPoint
    {
        [IndexField("name_t")]
        public string Name { get; set; }
        [IndexField("address_t")]
        public string Address { get; set; }
        [IndexField("location_t")]
        public string Location { get; set; }
    }
}