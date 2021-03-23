namespace Corlate.Foundation.Common.Utilities
{
    using Sitecore;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;
    using Sitecore.Data.Templates;
    using Sitecore.Links;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Resources.Media;
    using Sitecore.SecurityModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Defines the <see cref="SitecoreUtility" />
    /// </summary>
    public class SitecoreUtility
    {
        /// <summary>
        /// check if the ID is a valid item ID
        /// </summary>
        /// <param name="id">The <see cref="ID"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsValidID(ID id)
        {
            try
            {
                return (!string.IsNullOrEmpty(System.Convert.ToString(id)) && !id.IsNull && !id.IsGlobalNullId);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// check if the item has this field
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldID"></param>
        /// <returns></returns>
        public static bool ItemHasField(Item item, ID fieldID)
        {
            if (item != null && IsValidID(fieldID))
            {
                /*For the sake of performance, Sitecore will not give you all fields in the FieldCollection. only fields with explicit values on item level, including empty string.
                  However, you will be able to access fields with either Sitecore.Context.Item.Fields["title"]
                  In order to have all the fields in the FieldCollection and iterate through them, make sure your code will include Sitecore.Data.Items.Item.Fields.ReadAll() call before your foreach.
                 */
                item.Fields.ReadAll();
                Field field = item.Fields.Where(x => x.ID == fieldID).FirstOrDefault();
                return field != null;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// gets the list of selected items in a multilist field
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldID"></param>
        /// <returns></returns>
        public static IList<Item> GetSelectedItemsInMultilistField(Item contextItem, ID multilistFieldID)
        {
            if (contextItem != null && IsValidID(multilistFieldID) && ItemHasField(contextItem, multilistFieldID))
            {
                return new MultilistField(contextItem.Fields[multilistFieldID]).GetItems();
            }

            return null;
        }

        /// <summary>
        /// get the url of the media item (e.g image)
        /// </summary>
        /// <param name="contextItem"></param>
        /// <param name="mediaItemFieldID"></param>
        /// <param name="includeServerURL"></param>
        /// <returns></returns>
        public static string GetMediaItemURL(Item contextItem, ID mediaItemFieldID, bool includeServerURL = false)
        {
            string mediaItemURL = string.Empty;

            if (contextItem != null && IsValidID(mediaItemFieldID) && ItemHasField(contextItem, mediaItemFieldID))
            {
                ImageField imageField = contextItem.Fields[mediaItemFieldID];

                if (imageField?.MediaItem != null)
                {
                    var image = new MediaItem(imageField.MediaItem);
                    MediaUrlOptions muo = new MediaUrlOptions();
                    muo.AlwaysIncludeServerUrl = includeServerURL;
                    mediaItemURL = MediaManager.GetMediaUrl(image, muo);
                }
            }

            return mediaItemURL;
        }

        /// <summary>
        /// get the item from context database
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public static Item GetItem(ID itemID)
        {
            Database db = Sitecore.Configuration.Factory.GetDatabase(Context.Database.Name);

            using (new SecurityDisabler())
            {
                return db.GetItem(itemID);
            }
        }

        /// <summary>
        /// get the host name from current URL
        /// </summary>
        /// <returns></returns>
        public static string GetHostNameFromURL()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Url.PathAndQuery) && HttpContext.Current.Request.Url.PathAndQuery != "/")
                return HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.PathAndQuery, "");
            else
                return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// get the url of the general link field value
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldId"></param>
        /// <param name="getURLWithDomain"></param>
        /// <returns></returns>
        public static string GetLinkURL(Item item, ID fieldId, bool getURLWithDomain = false)
        {
            if (item == null || fieldId.IsNull || string.IsNullOrEmpty(System.Convert.ToString(fieldId)))
            {
                return string.Empty;
            }

            LinkField linkField = item.Fields[fieldId.ToString()];

            if (linkField == null)
            {
                return string.Empty;
            }

            string url = string.Empty;

            switch (linkField.LinkType.ToLower())
            {
                case "internal":
                    // Use LinkMananger for internal links, if link is not empty
                    url = linkField.TargetItem != null ? LinkManager.GetItemUrl(linkField.TargetItem) : string.Empty;
                    break;
                case "media":
                    // Use MediaManager for media links, if link is not empty
                    url = linkField.TargetItem != null ? MediaManager.GetMediaUrl(linkField.TargetItem) : string.Empty;
                    break;
                case "external":
                    // Just return external links
                    url = linkField.Url;
                    break;
                case "anchor":
                    // Prefix anchor link with # if link if not empty
                    url = !string.IsNullOrEmpty(linkField.Anchor) ? "#" + linkField.Anchor : string.Empty;
                    break;
                case "mailto":
                    // Just return mailto link
                    url = linkField.Url;
                    break;
                case "javascript":
                    // Just return javascript
                    url = linkField.Url;
                    break;
                default:
                    // Just please the compiler, this
                    // condition will never be met
                    url = linkField.Url;
                    break;
            }

            if (!string.IsNullOrEmpty(url) && getURLWithDomain)
            {
                url = url.StartsWith("/") ? url : "/" + url;
                url = GetHostNameFromURL() + url;
            }

            return url;
        }

        /// <summary>
        /// get the text entered in the "Description" field of a general link field
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public static string GetLinkDescription(Item item, ID fieldId)
        {
            if (item == null || !IsValidID(fieldId) || !ItemHasField(item, fieldId))
            {
                return string.Empty;
            }

            LinkField field = item.Fields[fieldId];
            return field != null ? field.Text : string.Empty;
        }

        /// <summary>
        /// gets the datasource of the current rendering
        /// </summary>
        /// <returns></returns>
        public static Item GetRenderingDatasourceItem()
        {
            if (Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull != null)
            {
                return Sitecore.Context.Database.GetItem(Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull.Rendering.DataSource);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// get the URL of the item
        /// </summary>
        /// <param name="contextItem"></param>
        /// <param name="getURLWithDomain"></param>
        /// <returns></returns>
        public static string GetItemURL(Item contextItem, bool getURLWithDomain = false)
        {
            string url = string.Empty;

            if (contextItem != null)
            {
                var pathInfo = LinkManager.GetItemUrl(contextItem, UrlOptions.DefaultOptions);
                url = pathInfo.TrimStart(new char[] { '/' });
            }

            url = url.StartsWith("/") ? url : "/" + url;

            return getURLWithDomain ? GetHostNameFromURL() + url : url;
        }

        /// <summary>
        /// get all the child items of a given template id, under a parent item
        /// </summary>
        /// <param name="parentItem"></param>
        /// <param name="childTemplateID"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public static List<Item> GetItemsByTemplate(Item parentItem, ID templateID, bool checkBaseTemplates = false)
        {
            List<Item> itemsByTemplate = new List<Item>();

            Item[] childItemsByTemplateID = null;

            if (parentItem != null && IsValidID(templateID))
            {
                //if the item path is like - /sitecore/content/home-page, 
                //then the correct xpath query will be /sitecore/content/#home-page#
                string[] nodes = parentItem.Paths.FullPath.Split('/');

                if (nodes != null && nodes.Length > 0)
                {
                    StringBuilder itemPath = new StringBuilder();

                    foreach (string itemName in nodes)
                    {
                        if (itemName.Contains("-") || itemName.Contains("and") || itemName.Contains("or"))
                        {
                            itemPath.Append("/#" + itemName + "#");
                        }
                        else
                        {
                            itemPath.Append("/" + itemName);
                        }
                    }

                    string query = "" + System.Convert.ToString(itemPath).TrimEnd('/') + "//*[@@templateid='" + templateID + "']";
                    childItemsByTemplateID = Sitecore.Context.Database.SelectItems(query);
                }
            }

            if (childItemsByTemplateID != null && childItemsByTemplateID.Length > 0)
            {
                itemsByTemplate = childItemsByTemplateID.ToList();
            }

            return itemsByTemplate;
        }

        /// <summary>
        /// get the preceeding sibling of an item based on template id
        /// </summary>
        /// <param name="referenceItem"></param>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public static Item GetPreceedingSiblingItemByTemplate(Item referenceItem, ID templateID, ID isActiveFieldID)
        {
            Item[] list = referenceItem.Axes.SelectItems("./preceding::*[@@templateid='" + templateID.ToString() + "']");
            return list != null && list.Length > 0 ? list.Where(x => x.Fields[isActiveFieldID].Value == "1").LastOrDefault() : null;
        }

        /// <summary>
        /// get the succeeding sibling of an item based on template id
        /// </summary>
        /// <param name="referenceItem"></param>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public static Item GetSucceedingSiblingItemByTemplate(Item referenceItem, ID templateID, ID isActiveFieldID)
        {
            Item[] list = referenceItem.Axes.SelectItems("./following::*[@@templateid='" + templateID.ToString() + "']");
            return list != null && list.Length > 0 ? list.Where(x => x.Fields[isActiveFieldID].Value == "1").FirstOrDefault() : null;
        }

        /// <summary>
        /// checks whether an item has been referrred by other items of certain template.
        /// </summary>
        /// <param name="contextItem"></param>
        /// <param name="referrerTemplateID"></param>
        /// <param name="isActiveFieldID"></param>
        /// <returns></returns>
        public static bool HasReferrers(Item contextItem, ID referrerTemplateID, ID isActiveFieldID)
        {
            bool hasReferrers = false;

            if (contextItem != null)
            {
                string databaseName = Sitecore.Context.Database.Name.ToLower();
                // getting all linked Items that refer to the Item
                List<ItemLink> itemLinks = Globals.LinkDatabase.GetReferrers(contextItem).Where(x => x.SourceDatabaseName.ToLower() == databaseName).ToList();
                
                if (itemLinks != null && itemLinks.Count > 0)
                {
                    foreach (ItemLink itemLink in itemLinks)
                    {
                        Item linkItem = GetItem(itemLink.SourceItemID);

                        if (linkItem != null && linkItem.TemplateID == referrerTemplateID && linkItem.Fields[isActiveFieldID].Value == "1")
                        {
                            hasReferrers = true;
                            break;
                        }
                    }                    
                } 
            }

            return hasReferrers;
        }

        /// <summary>
        /// Get the field value of rendering parameter
        /// </summary>
        /// <param name="renderingParametersTemplateID"></param>
        /// <param name="renderingParametersTemplateFieldID"></param>
        /// <returns></returns>
        public static string GetRenderingParameters(ID renderingParametersTemplateID, ID renderingParametersTemplateFieldID)
        {
            string parametersTemplateFieldNameInRendering = "Parameters Template";
            string renderingParameters = string.Empty;
            Rendering currentRendering = RenderingContext.Current.Rendering;

            if (currentRendering != null)
            {
                Item currentRenderingItem = GetItem(currentRendering.RenderingItem.ID);

                if (currentRenderingItem != null && currentRenderingItem.Fields[parametersTemplateFieldNameInRendering] != null &&
                    currentRenderingItem.Fields[parametersTemplateFieldNameInRendering].Value == System.Convert.ToString(renderingParametersTemplateID))
                {
                    Template template = TemplateManager.GetTemplate(renderingParametersTemplateID, Context.Database);
                    TemplateField templateField = template.GetFields(true).Where(x => x.ID == renderingParametersTemplateFieldID).FirstOrDefault();
                    renderingParameters = templateField != null ? currentRendering.Parameters[templateField.Name] : string.Empty;
                }
            }

            return renderingParameters;
        }

        /// <summary>
        /// get pagewise records from a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstOfRecords">The list of records to be checked</param>
        /// <param name="maxRecordsPerPage">Max records per page</param>
        /// <param name="selectedPageNumber">selected page number</param>
        /// <param name="totalPages"> total pages on calculation</param>
        /// <returns></returns>
        public static List<T> GetPagewiseRecords<T>(List<T> lstOfRecords, int maxRecordsPerPage, int selectedPageNumber, out int totalPages)
        {
            // define return list
            List<T> lst = new List<T>();
            totalPages = 1;
            
            if (lstOfRecords != null && lstOfRecords.Count() > 0)
            {
                int totalRecords = lstOfRecords.Count();
                totalPages = (totalRecords + maxRecordsPerPage - 1) / maxRecordsPerPage;
                selectedPageNumber = selectedPageNumber > totalPages ? totalPages : selectedPageNumber;
                int startIndex = (selectedPageNumber * maxRecordsPerPage) - maxRecordsPerPage;
                startIndex = (startIndex >= 0 && startIndex < totalRecords) ? startIndex : 0;
                int numberOfRecordsAvailableForPage = (maxRecordsPerPage <= (totalRecords - startIndex)) ? maxRecordsPerPage : (totalRecords - startIndex);
                lst = lstOfRecords.ToList().GetRange(startIndex, numberOfRecordsAvailableForPage);
            }

            return lst;
        }

        /// <summary>
        /// gets the template item from master database
        /// </summary>
        /// <param name="templatePath"></param>
        /// <returns></returns>
        public static TemplateItem GetTemplateItem(string templateID)
        {
            TemplateItem templateItem = null;

            if (!string.IsNullOrEmpty(templateID))
            {
                Item item = GetItem(new ID(templateID));

                if (item != null)
                {
                    string templatePath = item.Paths.FullPath;
                    templatePath = templatePath.Trim().TrimStart('/');
                    templatePath = !string.IsNullOrEmpty(templatePath) && templatePath.ToLower().StartsWith("sitecore/templates/") ? templatePath.ToLower().Replace("sitecore/templates/", "") : templatePath;
                    templatePath = !string.IsNullOrEmpty(templatePath) ? templatePath.Trim().TrimEnd('/') : templatePath;
                    templateItem = !string.IsNullOrEmpty(templatePath) ? Factory.GetDatabase("master").GetTemplate(templatePath) : null; 
                }
            }

            return templateItem;
        }

        /// <summary>
        /// create an item under a parent in the master DB and update its field values
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="parentItem"></param>
        /// <param name="templateID"></param>
        /// <param name="listOfItemFieldsWithValues"></param>
        /// <returns></returns>
        public static Item CreateItem(string itemName, ID parentItemID, string templateID, List<KeyValuePair<ID, string>> listOfItemFieldsWithValues)
        {
            Item newItem = null;
            Item parentItem = Factory.GetDatabase("master").GetItem(parentItemID);

            if (!string.IsNullOrEmpty(itemName) && parentItem != null && !string.IsNullOrEmpty(templateID))
            {
                TemplateItem templateItem = GetTemplateItem(templateID);

                if (templateItem != null)
                {
                    using (new SecurityDisabler())
                    {
                        newItem = parentItem.Add(itemName, templateItem);

                        if (newItem != null)
                        {
                            newItem.Editing.BeginEdit();

                            if (listOfItemFieldsWithValues != null && listOfItemFieldsWithValues.Count > 0)
                            {
                                foreach (KeyValuePair<ID, string> kvp in listOfItemFieldsWithValues)
                                {
                                    if (ItemHasField(newItem, kvp.Key))
                                    {
                                        newItem[kvp.Key] = kvp.Value;
                                    }
                                }
                            }

                            newItem.Editing.EndEdit();
                        }
                    }  
                }
            }

            return newItem;
        }

        /// <summary>
        /// generate a random string
        /// </summary>
        /// <param name="randomNumberLength"></param>
        /// <returns></returns>
        public static string GenerateRandomNumber(int randomNumberLength)
        {

            //This one tells you how many characters the string will contain.

            //This one, is empty for now - but will ultimately hold the finised randomly generated password
            string NewPassword = "";

            //This one tells you which characters are allowed in this new password
            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";            

            //Then working with an array...

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);

            //string IDString = "";
            string temp = "";

            //utilize the "random" class
            Random rand = new Random();

            //and lastly - loop through the generation process...
            for (int i = 0; i < System.Convert.ToInt32(randomNumberLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                NewPassword += temp;                
            }

            return NewPassword;
        }
    }
}
