using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Feature.Navigation.Controllers;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Navigation.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(NavigationController));
        }
    }
}