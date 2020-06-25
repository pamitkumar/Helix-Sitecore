using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NISSitecore.Feature.Navigation.Controllers
{
    public class NavigationController : SitecoreController
    {
        // GET: Navigation
        public ActionResult Sitemap()
        {
            var refObj = Sitecore.Configuration.Factory.CreateObject("guide/dictionarymap", true) as NISSitecore.Feature.Navigation.Configuration.MappingComponent;

            foreach (KeyValuePair<Guid,string> entry in refObj.ItemNames)
            {
                var guidItem = entry.Key;
                var name = entry.Key;
            }  

           

            return View("~/Views/Navigation/Sitemap.cshtml");
        }
    }
}