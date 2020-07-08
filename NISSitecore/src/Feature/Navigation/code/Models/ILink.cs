using Sitecore.Shell.Applications.ContentEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Navigation.Models
{
    public interface ILink:INavigationGlassBase
    {
        Link Link { get; set; }
    }
}
