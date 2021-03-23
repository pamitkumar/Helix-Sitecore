
namespace Corlate.Feature.PageContent.Controllers
{
    using Corlate.Feature.PageContent.Models;
    using Corlate.Foundation.Common.Models;
    using Corlate.Foundation.Common.Utilities;
    using Sitecore.Data.Items;
    using System.Web.Mvc;

    public class RichTextSectionController : Controller
    {
        public ActionResult RenderRichTextSection()
        {
            RichTextSection viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.RichTextSection.ID)
                {
                    viewModel = new RichTextSection(renderingDatasourceItem);

                    if (!viewModel.IsActive)
                    {
                        viewModel = null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/RichTextSection.cshtml", viewModel);
        }
    }
}