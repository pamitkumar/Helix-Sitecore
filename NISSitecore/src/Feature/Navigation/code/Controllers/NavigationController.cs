using NISSitecore.Feature.Navigation.Models;
using NISSitecore.Foundation.Content.Repositories;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Extensions;
using Sitecore.Web.UI.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NISSitecore.Feature.Navigation.Controllers
{
    public class NavigationController : SitecoreController
    {
        private readonly IContentRepository _contentRepository;
        private readonly IRenderingRepository _renderingRepository;
        private readonly IContextRepository _contextRepository;

        public NavigationController(IRenderingRepository renderingRepository,IContentRepository contentRepository,IContextRepository contextRepository)
        {
            _renderingRepository = renderingRepository;
            _contentRepository = contentRepository;
            _contextRepository = contextRepository;
        }
        
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

        public ActionResult PrimaryMenu()
        {
            var datasource = _renderingRepository.GetRenderingItem<IListItems>();
            var root = _contextRepository.GetHomeItem<IList>();
            

            //var root = (string.IsNullOrEmpty(datasource) ? context.GetHomeItem<Models.CorporateMenuItem>(false, true) : context.GetItem<Models.CorporateMenuItem>(navDataSource));

            if (datasource != null)
            {
                //var count = datasource.Children.Count();
                //foreach(IList child in datasource.Children)
                //{
                //    var displayField = child.ShowInNavigation;
                //    if(displayField)
                //    {

                //    }
                //}
                datasource.Children = root.MenuChildren;
                datasource.ActiveItemId = _contentRepository.ContextItem.ID.Guid;
                datasource.ActiveAncestors = new List<Item>(_contentRepository.ContextItem.Axes.GetAncestors());

            }
            return View(datasource);
        }
    }
}