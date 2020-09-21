using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Feature.Search.Controllers;
using NISSitecore.Feature.Search.Repository;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Search.DI
{
    public class RegisterContainer: IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(SearchResultsController));
            serviceCollection.AddTransient<ISearchServiceRepository, SearchServiceRepository>();
        }
    }
}