using Sitecore.Data.Items;
using System.Web;

namespace NISSitecore.Foundation.Abstractions.Services
{
    public interface ITrackingService
    {
        void RegisterEvent(string query, int count);
        Item GetPreviousPageItem(Item currentItem);
        HtmlString GetVisitorIdentification();
    }
}
