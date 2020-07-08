using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Feature.Metadata.Controllers;
using NISSitecore.Feature.Metadata.Mediators;
using NISSitecore.Feature.Metadata.Models;
using NISSitecore.Feature.Metadata.Services;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Metadata.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(MetadataController));
            serviceCollection.AddTransient<IMetadaMediator, MetadataMediator>();
            serviceCollection.AddTransient<IMetadataService, MetadataService>();
        }
    }
}