using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Foundation.Abstractions;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Globalization;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines;
using Sitecore.Web;

namespace NISSitecore.Foundation.SitecoreExtensions.Extensions
{
    public static class ArgsExtensions
    {
        public static Item GetContextItem(this PipelineArgs args)
        {
            IContext service = (IContext)ServiceLocator.ServiceProvider.GetService(typeof(IContext));
            if (!(args.CustomData["contextItem"] is Item obj))
                obj = service.Item;
            Item obj1 = obj;
            if (obj1 != null)
                return obj1;
            Language language = Language.Parse(WebUtil.GetFormValue("language"));
            ID shortId = ArgsExtensions.SafeParseShortId(WebUtil.GetFormValue("itemid"));
            if (service.ContentDatabase != null && language != (Language)null && !shortId.IsNull)
                return service.ContentDatabase.GetItem(shortId, language);
            return PageContext.CurrentOrNull != null && PageContext.CurrentOrNull.Item != null ? PageContext.Current.Item : (Item)null;
        }

        private static ID SafeParseShortId(string raw)
        {
            return string.IsNullOrEmpty(raw) ? ID.Null : ShortID.DecodeID(raw);
        }
    }
}