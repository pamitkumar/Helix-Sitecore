using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Feature.Maps.Controllers;
using NISSitecore.Feature.Maps.Repositories;
using Sitecore.DependencyInjection;

namespace NISSitecore.Feature.Maps.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(MapsController));
            serviceCollection.AddTransient<IMapPointRepository, MapPointRepository>();            
        }
    }
}