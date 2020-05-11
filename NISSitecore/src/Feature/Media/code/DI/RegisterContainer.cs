using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Feature.Media.Controllers;
using NISSitecore.Feature.Media.Mediators;
using NISSitecore.Feature.Media.Services;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Media.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(MediaController));
            serviceCollection.AddTransient<ICarouselMediator, CarouselMediator>();
            serviceCollection.AddTransient<ICarouselService, CarouselService>();
        }
    }
}