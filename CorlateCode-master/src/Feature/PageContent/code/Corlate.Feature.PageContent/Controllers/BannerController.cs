

namespace Corlate.Feature.PageContent.Controllers
{
    using Foundation.Common.Models;
    using Foundation.Common.Utilities;
    using Models;
    using Sitecore.Data.Items;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    public class BannerController : Controller
    {
        public ActionResult RenderBanner()
        {
            Banner banner = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.Banner.ID)
                {
                    banner = new Banner(renderingDatasourceItem);

                    if (!banner.IsActive)
                    {
                        banner = null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/Banner.cshtml", banner);
        }

        public ActionResult RenderBannerCarousel()
        {
            BannerCarouselSource viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.BannerCarouselSource.ID)
                {
                    viewModel = new BannerCarouselSource(renderingDatasourceItem);

                    if (!viewModel.IsActive)
                    {
                        viewModel = null;
                    }
                    else
                    {
                        //get all the active carousel slide items selected in the multilist field
                        List<Item> selectedItems = SitecoreUtility.GetSelectedItemsInMultilistField(renderingDatasourceItem, References.Templates.BannerCarouselSource.Fields.Slides)
                            .Where(x => x.Fields[References.Templates.BannerCarouselSlide.Fields.IsActive].Value == "1").ToList();

                        if(selectedItems != null && selectedItems.Count > 0)
                        {
                            viewModel.Slides = new List<BannerCarouselSlide>();

                            foreach(Item item in selectedItems)
                            {
                                BannerCarouselSlide featuredItem = new BannerCarouselSlide(item);
                                viewModel.Slides.Add(featuredItem);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/BannerCarousel.cshtml", viewModel);
        }
    }
}