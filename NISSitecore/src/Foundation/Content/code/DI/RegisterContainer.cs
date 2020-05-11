using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Foundation.Content.Repositories;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Foundation.Content.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IContentRepository, ContentRepository>();

            // Allow IContentRepository to be resolved on-demand by singletons
            serviceCollection.AddSingleton<Func<IContentRepository>>(_ => () => ServiceLocator.ServiceProvider.GetService<IContentRepository>());

            serviceCollection.AddTransient<IRenderingRepository, RenderingRepository>();

            serviceCollection.AddTransient<IContextRepository, ContextRepository>();

            // Allow IContextRepository to be resolved on-demand by singletons
            serviceCollection.AddSingleton<Func<IContextRepository>>(_ => () => ServiceLocator.ServiceProvider.GetService<IContextRepository>());
        }
    }
}