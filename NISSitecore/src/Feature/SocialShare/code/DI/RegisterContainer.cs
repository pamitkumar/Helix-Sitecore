using Microsoft.Extensions.DependencyInjection;

using NISSitecore.Feature.SocialShare.Repository;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.SocialShare.DI
{
    public class RegisterContainer: IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISocialShare, IDBSocialShare>();
        }
    }
}