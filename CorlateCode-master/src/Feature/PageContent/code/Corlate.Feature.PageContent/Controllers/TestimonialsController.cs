  
namespace Corlate.Feature.PageContent.Controllers
{
    using Corlate.Feature.PageContent.Models;
    using Corlate.Foundation.Common.Models;
    using Corlate.Foundation.Common.Utilities;
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class TestimonialsController : Controller
    {
        public ActionResult RenderTestimonials()
        {
            TestimonialsSource viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.TestimonialsSource.ID)
                {
                    viewModel = new TestimonialsSource(renderingDatasourceItem);

                    if (!viewModel.IsActive)
                    {
                        viewModel = null;
                    }
                    else
                    {
                        List<Item> selectedItems = SitecoreUtility.GetSelectedItemsInMultilistField(renderingDatasourceItem, References.Templates.TestimonialsSource.Fields.Testimonials)
                            .Where(x => x.Fields[References.Templates.UserTestimonial.Fields.IsActive].Value == "1").ToList();

                        if (selectedItems != null && selectedItems.Count > 0)
                        {
                            viewModel.Testimonials = new List<UserTestimonial>();

                            foreach (Item item in selectedItems)
                            {
                                UserTestimonial featuredItem = new UserTestimonial(item);
                                viewModel.Testimonials.Add(featuredItem);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/Testimonials.cshtml", viewModel);
        }
    }
}