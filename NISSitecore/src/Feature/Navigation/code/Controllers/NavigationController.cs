using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
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

        public ActionResult BreadCrumbs()
        {
            //var context = new IRequestContext();
            Item rootItem = Sitecore.Context.Database.Items[Sitecore.Context.Site.StartPath].Parent;
            Item currentItem = Sitecore.Context.Item;

            //var datasource = _renderingRepository.GetRenderingItem<IListItems>();
            //var root = _contextRepository.GetHomeItem<IList>();

            ViewModels.BreadcrumbsViewModel model = new ViewModels.BreadcrumbsViewModel();
            model.Trail = new List<Models.IList>();
            model.ActiveItemId = currentItem.ID.Guid;

            while (currentItem != null && currentItem.ID != rootItem.ID)
            {
                var options = new GetItemByIdOptions(currentItem.ID.Guid)
                {
                    InferType = true,
                    Lazy = Glass.Mapper.LazyLoading.Disabled
                };
                Models.IList currentMenuItem = _contentRepository.GetItem<Models.IList>(options);
                if (currentMenuItem == null)
                {
                    break;
                }
                
                model.Trail.Add(currentMenuItem);


                //if current item says hide from breadcrumbs then turn off breadcrumbs for entire path.
                if (currentMenuItem.Id == model.ActiveItemId && currentMenuItem.HideFromBreadcrumbs)
                {
                    model.Trail = new List<Models.IList>();
                    model.HideBreadcrumb = true;
                    break;
                }

                //skip over items above it in chain;
                if (currentMenuItem.HideFromBreadcrumbs)
                    model.Trail.RemoveAt(model.Trail.Count - 1);
                currentItem = currentItem.Parent;
            }
            if (model.HideBreadcrumb == false && model.Trail.Count > 1)
                model.Trail.Reverse();

            
            return View(model);

        }
    }
}