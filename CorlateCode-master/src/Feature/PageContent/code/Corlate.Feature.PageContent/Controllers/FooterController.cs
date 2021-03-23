

namespace Corlate.Feature.PageContent.Controllers
{
    using Foundation.Common.Models;
    using Foundation.Common.Utilities;
    using Models;
    using ProjectConfigurations.Models;
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class FooterController : Controller
    {
        // GET: Footer
        public ActionResult RenderFooter()
        {
            FooterSource viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.FooterSource.ID)
                {
                    viewModel = new FooterSource(renderingDatasourceItem);

                    if (viewModel.IsActive)
                    {
                        viewModel.Identity = new Identity(viewModel.IdentitySourceItem);

                        //get the list of items selected in the multilist field
                        IList<Item> featuredItems = SitecoreUtility.GetSelectedItemsInMultilistField(viewModel.InnerItem, References.Templates.FooterSource.Fields.SocialLinks);

                        //check for each item if it is of valid template and active.If yes, then add it to the list
                        if (featuredItems != null && featuredItems.Count > 0)
                        {
                            viewModel.SocialLinks = new List<FooterSocialLink>();

                            foreach (Item featuredItem in featuredItems)
                            {
                                if (featuredItem.TemplateID == References.Templates.FooterSocialLink.ID)
                                {
                                    FooterSocialLink oblFeaturedItem = new FooterSocialLink(featuredItem);

                                    if (oblFeaturedItem != null && oblFeaturedItem.IsActive)
                                    {
                                        viewModel.SocialLinks.Add(oblFeaturedItem);
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

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/Footer.cshtml", viewModel);
        }
    }
}