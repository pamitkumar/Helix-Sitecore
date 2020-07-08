using Glass.Mapper.Sc.Configuration.Attributes;
using NISSitecore.Foundation.ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Navigation.Models
{
    [SitecoreType(TemplateId = "{A1CBA309-D22B-46D5-80F8-2972C185363F}", AutoMap =true)]
    public interface IList:IGlassBase
    {
        [SitecoreChildren(InferType = true)]
        IEnumerable<IList> MenuChildren { get; set; }
        [SitecoreField("NavigationTitle")]
        string NavigationTitle { get; set; }
        [SitecoreField("ShowInNavigation")]
        bool ShowInNavigation { get; set; }
        [SitecoreField("ShowChildren")]
        bool ShowChildren { get; set; }
    }
}