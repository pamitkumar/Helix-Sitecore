using Glass.Mapper.Sc.Pipelines.AddMaps;
using NISSitecore.Foundation.ORM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Media.ORM
{
    public class RegisterMappings : AddMapsPipeline
    {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("NSSitecore.Feature.Media");
        }
    }
}