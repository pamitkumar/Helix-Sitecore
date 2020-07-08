using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Media.Models
{
    [SitecoreType]
    public class BaseItem
    {  
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreInfo(Glass.Mapper.Sc.Configuration.SitecoreInfoType.Name)]
        public virtual string Name { get; set; }
    }
}