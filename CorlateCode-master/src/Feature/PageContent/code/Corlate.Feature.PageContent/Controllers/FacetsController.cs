
namespace Corlate.Feature.PageContent.Controllers
{
    using Corlate.Feature.PageContent.Models;
    using Corlate.Foundation.Common.Models;
    using Corlate.Foundation.Common.Utilities;
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class FacetsController : Controller
    {
        public ActionResult RenderFacets()
        {
            FacetsSource viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.FacetsSource.ID)
                {
                    viewModel = new FacetsSource(renderingDatasourceItem);

                    if (!viewModel.IsActive)
                    {
                        viewModel = null;
                    }
                    else
                    {
                        List<Item> selectedItems = SitecoreUtility.GetSelectedItemsInMultilistField(renderingDatasourceItem, References.Templates.FacetsSource.Fields.Facets)
                            .Where(x => x.Fields[References.Templates.Facet.Fields.IsActive].Value == "1").ToList();

                        if (selectedItems != null && selectedItems.Count > 0)
                        {
                            viewModel.Facets = new List<Facet>();

                            foreach (Item item in selectedItems)
                            {
                                Facet featuredItem = new Facet(item);
                                viewModel.Facets.Add(featuredItem);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/Facets.cshtml", viewModel);
        }
    }
}