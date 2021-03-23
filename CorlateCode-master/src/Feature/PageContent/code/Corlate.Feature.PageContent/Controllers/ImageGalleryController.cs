
namespace Corlate.Feature.PageContent.Controllers
{
    using Foundation.Common.Models;
    using Foundation.Common.Utilities;
    using Models;
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ImageGalleryController : Controller
    {
        public ActionResult RenderImageGallery()
        {
            ImageGallerySource viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.ImageGallerySource.ID)
                {
                    viewModel = new ImageGallerySource(renderingDatasourceItem);

                    if (viewModel.IsActive)
                    {
                        //get the list of items selected in the multilist field
                        IList<Item> featuredItems = SitecoreUtility.GetSelectedItemsInMultilistField(viewModel.InnerItem, References.Templates.ImageGallerySource.Fields.GalleryItems);

                        //check for each item if it is of valid template and active.If yes, then add it to the list
                        if (featuredItems != null && featuredItems.Count > 0)
                        {
                            viewModel.GalleryItems = new List<ImageGalleryItem>();

                            foreach (Item featuredItem in featuredItems)
                            {
                                if (featuredItem.TemplateID == References.Templates.ImageGalleryItem.ID)
                                {
                                    ImageGalleryItem imageGalleryItem = new ImageGalleryItem(featuredItem);

                                    if (imageGalleryItem != null && imageGalleryItem.IsActive)
                                    {
                                        viewModel.GalleryItems.Add(imageGalleryItem);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        viewModel = null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/ImageGallery.cshtml", viewModel);
        }
    }
}