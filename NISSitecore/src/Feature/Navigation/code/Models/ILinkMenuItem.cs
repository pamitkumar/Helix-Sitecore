using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Navigation.Models
{
    public interface ILinkMenuItem:INavigationGlassBase
    {
        string Icon { get; set; }
        bool DividerBefore { get; set; }
    }
}
