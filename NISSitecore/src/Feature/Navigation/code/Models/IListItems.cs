using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Navigation.Models
{
    public interface IListItems:INavigationGlassBase
    {
        [SitecoreChildren]
        IEnumerable<IList> Children { get; set; }
        List<Sitecore.Data.Items.Item> ActiveAncestors { get; set; }
        Guid ActiveItemId { get; set; }
    }
}