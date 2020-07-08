using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Navigation.Models
{
    public interface INavigable:INavigationGlassBase
    {
        string NavigationTitle { get; set; }

        bool ShowInNavigation { get; set; }

        bool ShowChildren { get; set; }
    }
}
