using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Foundation.DI.Extensions;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Foundation.DI
{
    public class RegisterControllers : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllers("NSSitecore.Feature.*");
        }
    }
}