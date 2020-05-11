using Glass.Mapper.Sc.Pipelines.AddMaps;
using NISSitecore.Foundation.ORM.Extensions;

namespace NISSitecore.Foundation.ORM.Mappings
{
    public class RegisterMappings:AddMapsPipeline
    {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("NSSitecore.Foundation.ORM");
        }
    }
}