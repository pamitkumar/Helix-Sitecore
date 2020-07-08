using NISSitecore.Feature.Navigation.Models;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Navigation.Repositories
{
    public interface INavigationRepository
    {
        Item GetNavigationRoot(Item contextItem);

        IEnumerable<INavigable> GetPrimaryMenu();
    }
}
