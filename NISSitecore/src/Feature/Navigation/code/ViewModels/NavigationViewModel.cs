using Glass.Mapper.Sc.Fields;
using NISSitecore.Feature.Navigation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Navigation.ViewModels
{
    public class NavigationViewModel
    {        
        public Link Root { get; set; }
        public IEnumerable<INavigable> MenuItems { get; set; }
        public List<Sitecore.Data.Items.Item> ActiveAncestors { get; set; }
        public Guid ActiveItemId { get; set; }
    }
}