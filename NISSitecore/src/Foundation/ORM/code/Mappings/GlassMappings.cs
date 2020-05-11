using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Maps;
using NISSitecore.Foundation.ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Foundation.ORM.Mappings
{
    public class GlassMappings : SitecoreGlassMap<IGlassBase>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
                config.Info(f => f.BaseTemplateIds).InfoType(SitecoreInfoType.BaseTemplateIds);                
            });
        }
    }
}