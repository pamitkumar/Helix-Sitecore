using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Web;
using NISSitecore.Foundation.ORM.Models;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NISSitecore.Foundation.Content.Repositories
{
    public class ContentRepository:IContentRepository
    {
        private readonly IRequestContext _requestContext;

        public ContentRepository(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public T GetItem<T>(GetItemOptions options) where T : class
        {
            return _requestContext.SitecoreService.GetItem<T>(options);
        }

        public object GetItem(GetItemOptions options)
        {
            return _requestContext.SitecoreService.GetItem(options);
        }

        public IEnumerable<T> GetItems<T>(GetItemsOptions options) where T : class
        {
            return _requestContext.SitecoreService.GetItems<T>(options);
        }

        public object GetItems(GetItemsOptions options)
        {
            return _requestContext.SitecoreService.GetItems(options);
        }

        public T CreateItem<T>(CreateOptions options) where T : class
        {
            return _requestContext.SitecoreService.CreateItem<T>(options);
        }

        public object CreateItem(CreateOptions options)
        {
            return _requestContext.SitecoreService.CreateItem(options);
        }

        public void SaveItem(SaveOptions options)
        {
            _requestContext.SitecoreService.SaveItem(options);
        }

        public void MoveItem(MoveByModelOptions options)
        {
            _requestContext.SitecoreService.MoveItem(options);
        }

        public void DeleteItem(DeleteByModelOptions options)
        {
            _requestContext.SitecoreService.DeleteItem(options);
        }

        public Item ContextItem => _requestContext.ContextItem;

        public IEnumerable<T> ChildrenOfType<T>(IGlassBase identifiable) where T : class
        {
            Guid templateId = GetTemplateIdFromConfiguration<T>();

            return ChildrenOfType<T>(identifiable, templateId);
        }       


        public IEnumerable<T> ChildrenOfType<T>(IGlassBase identifiable, Guid templateId) where T : class
        {
            if (templateId == Guid.Empty)
            {
                throw new Exception("An invalid or empty template id was specified");
            }

            Item item = _requestContext.SitecoreService.Database.GetItem(new ID(identifiable.Id),identifiable.Language);
            if (item == null)
            {
                throw new Exception("The item could not be found");
            }

            var children = item.GetChildren();

            if (children == null || children.Count == 0)
            {
                return Enumerable.Empty<T>();
            }

            ID templateIdToFind = new ID(templateId);
            Item[] resultant = children.Where(x => x.TemplateID == templateIdToFind).ToArray();

            return GetResults<T>(resultant);
        }
        private IEnumerable<T> GetResults<T>(Item[] resultant) where T : class
        {
            return resultant.Length > 0
                ? resultant.Select(x => _requestContext.SitecoreService.GetItem<T>(x))
                : Enumerable.Empty<T>();
        }

        private Guid GetTemplateIdFromConfiguration<T>() where T : class
        {
            Type type = typeof(T);
            SitecoreTypeConfiguration sitecoreTypeConfiguration =
                _requestContext.SitecoreService.GlassContext.GetTypeConfigurationFromType<SitecoreTypeConfiguration>(type);
            if (sitecoreTypeConfiguration == null)
            {
                throw new Exception("Glass does not know about this type");
            }

            Guid templateId = sitecoreTypeConfiguration.TemplateId.ToGuid();
            return templateId;
        }
    }
}