using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Foundation.Core.Services;
using Sitecore.DependencyInjection;

namespace NISSitecore.Foundation.Core.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMediatorService, MediatorService>();
        }
    }
}