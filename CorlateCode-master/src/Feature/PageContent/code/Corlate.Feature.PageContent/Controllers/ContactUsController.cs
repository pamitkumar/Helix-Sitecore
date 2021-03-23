
namespace Corlate.Feature.PageContent.Controllers
{
    using Corlate.Feature.PageContent.Models;
    using Corlate.Foundation.Common.Models;
    using Corlate.Foundation.Common.Utilities;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ContactUsController : Controller
    {
        public ActionResult RenderContactUsForm()
        {
            ContactUsFormSource viewModel = null;

            try
            {
                //get the datasource item assigned to the rendering
                Item renderingDatasourceItem = SitecoreUtility.GetRenderingDatasourceItem();

                if (renderingDatasourceItem != null && renderingDatasourceItem.TemplateID == References.Templates.ContactUsFormSource.ID)
                {
                    viewModel = new ContactUsFormSource(renderingDatasourceItem);

                    if (!viewModel.IsActive)
                    {
                        viewModel = null;
                    }
                    else
                    {
                        //get the list of items selected in the multilist field
                        IList<Item> featuredItems = SitecoreUtility.GetSelectedItemsInMultilistField(viewModel.InnerItem, References.Templates.ContactUsFormSource.Fields.SocialNetworkLinks);

                        //check for each item if it is of valid template and active.If yes, then add it to the list
                        if (featuredItems != null && featuredItems.Count > 0)
                        {
                            viewModel.SocialNetworkLinks = new List<FooterSocialLink>();

                            foreach (Item featuredItem in featuredItems)
                            {
                                if (featuredItem.TemplateID == References.Templates.FooterSocialLink.ID)
                                {
                                    FooterSocialLink oblFeaturedItem = new FooterSocialLink(featuredItem);

                                    if (oblFeaturedItem != null && oblFeaturedItem.IsActive)
                                    {
                                        viewModel.SocialNetworkLinks.Add(oblFeaturedItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
            }

            return View(GlobalConstants.SUBLAYOUTS_PATH + "PageContent/ContactUsForm.cshtml", viewModel);
        }

        /// <summary>
        /// get the form data and save as an item
        /// </summary>
        /// <param name="contactUsFormDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveContactUsFormData(ContactUsFormData contactUsFormDataModel)
        {
            string statusCode = "0";
            string statusMessage = string.Empty;

            try
            {
                List<KeyValuePair<ID, string>> fieldValueCollection = new List<KeyValuePair<ID, string>>();                
                fieldValueCollection.Add(new KeyValuePair<ID, string>(References.Templates.ContactUsFormData.Fields.FullName, contactUsFormDataModel.Fullname));
                fieldValueCollection.Add(new KeyValuePair<ID, string>(References.Templates.ContactUsFormData.Fields.Email, contactUsFormDataModel.Email));
                fieldValueCollection.Add(new KeyValuePair<ID, string>(References.Templates.ContactUsFormData.Fields.Subject, contactUsFormDataModel.Subject));
                fieldValueCollection.Add(new KeyValuePair<ID, string>(References.Templates.ContactUsFormData.Fields.Comments, contactUsFormDataModel.Comments));
                string itemName = contactUsFormDataModel.Email.Replace("@", "").Replace(".","") + SitecoreUtility.GenerateRandomNumber(5);
                
                Item newItem = SitecoreUtility.CreateItem(itemName, References.Content.ContactUsFormDataFolder, References.Templates.ContactUsFormData.ID.ToString(), fieldValueCollection);

                if (newItem != null)
                {
                    statusCode = "1";
                    statusMessage = "We've received your message and will get back to you shortly.";
                }
                else
                {
                    statusCode = "0";
                    statusMessage = "A problem occurred while processing your request. Please try again later.";
                }
            }
            catch (System.Exception ex)
            {
                LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
                statusCode = "0";
                statusMessage = "A problem occurred while processing your request. Please try again later.";
            }

            return Json(new { Status = statusCode, Message = statusMessage }, JsonRequestBehavior.AllowGet);
        }
    }
}