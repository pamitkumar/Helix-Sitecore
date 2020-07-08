using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using NISSitecore.Feature.Navigation.Models;
using NISSitecore.Feature.Navigation.ViewModels;
using NISSitecore.Foundation.Content.Repositories;
using NISSitecore.Foundation.ORM.Models;
using NISSitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NISSitecore.Feature.Navigation.Repositories
{
    public class NavigationRepository:INavigationRepository
    {
       

        private readonly IContextRepository _contextRepository;
        private readonly IRenderingRepository _renderingRepository;
        private readonly IContentRepository _icontentRepository;

        public Item ContextItem => RenderingContext.Current?.ContextItem ?? _contextRepository.GetCurrentItem<Item>();

        public Item NavigationRoot { get; }

        public NavigationRepository(IContextRepository contextRepository, IRenderingRepository renderingRepository, IContentRepository icontentRepository)
        {
            this.NavigationRoot = this.GetNavigationRoot(this.ContextItem);
            _contextRepository = contextRepository;
            _renderingRepository = renderingRepository;
            _icontentRepository = icontentRepository;
        }
        public Item GetNavigationRoot(Item contextItem)
        {                  
            return contextItem.GetAncestorOrSelfOfTemplate(Constants.NavigationRoot.TemplateId) ?? _contextRepository.GetCurrentItem<Item>();
        }

        public IEnumerable<INavigable> GetPrimaryMenu()
        {
            //    NavigationViewModel model = new NavigationViewModel();
            //    //model.MenuItems =_icontentRepository.ChildrenOfType<>
            return null;
        }

            IEnumerable<IGlassBase> BaseChildren { get; set; }
        public IEnumerable<T> GetChildren<T>() where T : class
        {
            var items = new List<T>();
           
            if (BaseChildren != null)
            {
                items.AddRange(
                      from item in BaseChildren
                      select _icontentRepository.GetItem<T>(new GetItemByIdOptions(item.Id)));
            }
            return items;
        }
    }
}