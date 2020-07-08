using Glass.Mapper.Sc.Pipelines.AddMaps;
using NISSitecore.Foundation.ORM.Extensions;

namespace NISSitecore.Feature.Navigation.ORM
{
    public class RegisterMappings : AddMapsPipeline
    {
        public void Process(AddMapsPipelineArgs args)
        {
            //var loader = new SitecoreAttributeConfigurationLoader("NISSitecore.Feature.Metadata");
            //args.MapsConfigFactory.Add();
            //args.Loaders.Add(loader);
            args.MapsConfigFactory.AddFluentMaps("NSSitecore.Feature.Navigation");
        }
    }
}