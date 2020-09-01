using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Navigation.ViewModels
{
    public class BreadcrumbsViewModel
    {
        public List<Models.IList> Trail { get; set; }
        public Guid ActiveItemId { get; set; }
        public bool HideBreadcrumb { get; set; }
    }
}