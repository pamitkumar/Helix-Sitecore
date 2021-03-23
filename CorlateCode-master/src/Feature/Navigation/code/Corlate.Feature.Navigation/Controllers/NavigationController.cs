
namespace Corlate.Feature.Navigation.Controllers
{
    using Foundation.Common.Models;
    using Foundation.Common.Utilities;
    using Models;
    using ProjectConfigurations.Models;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class NavigationController : Controller
    {
        public ActionResult RenderTopMenu()
        {
            TopMenuSource topMenuSource = null;             

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.TopMenuSource.ID &&
                    renderingDatasourceItem.HasChildren)
                {
                    topMenuSource = new TopMenuSource(renderingDatasourceItem);

                    if (topMenuSource != null)
                    {
                        if (topMenuSource.IdentitySourceItem != null)
                        {
                            topMenuSource.Identity = new Identity(topMenuSource.IdentitySourceItem);
                        }

                        topMenuSource.TopMenuItems = new List<NavigationItem>();

                        //loop through all the child items and add active menu items to the list
                        foreach (Item menuItem in renderingDatasourceItem.Children)
                        {
                            if (menuItem.TemplateID == References.Templates.NavigationItem.ID)
                            {
                                NavigationItem topMenuItem = new NavigationItem(menuItem);

                                if (topMenuItem.IsActive)
                                {
                                    if (menuItem.HasChildren)
                                    {
                                        topMenuItem.SubmenuItems = new List<NavigationItem>();

                                        //loop through all the sub items of the menu item and add active items to its subitem list
                                        foreach (Item submenuItem in menuItem.Children)
                                        {
                                            if (submenuItem.TemplateID == References.Templates.NavigationItem.ID)
                                            {
                                                NavigationItem smItem = new NavigationItem(submenuItem);

                                                if (smItem.IsActive)
                                                {
                                                    topMenuItem.SubmenuItems.Add(smItem);
                                                }
                                            }
                                        }
                                    }

                                    topMenuSource.TopMenuItems.Add(topMenuItem);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "Navigation/TopMenu.cshtml", topMenuSource);
        }
    }
}