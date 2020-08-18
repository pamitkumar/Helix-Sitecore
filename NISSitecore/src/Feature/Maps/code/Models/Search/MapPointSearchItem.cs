using Sitecore.ContentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Maps.Models.Search
{
    public class MapPointSearchItem:BaseSearchResultItem
    {
        [IndexField("mappointname_t")]
        public override string Name { get; set; }
        [IndexField("mappointaddress_t")]
        public string Address { get; set; }
        [IndexField("mappointlocation_t")]
        public new virtual string Location { get; set; }
    }
}