  
namespace Corlate.Feature.Navigation.Models
{
    using Corlate.Foundation.Common.Utilities;
    using Corlate.Feature.ProjectConfigurations.Models;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;

    public class TopMenuSource : CustomItem
    {
        public TopMenuSource(Item innerItem) : base(innerItem)
        {
        }

        public List<NavigationItem> TopMenuItems { get; set; }

        public Identity Identity { get; set; }

        /// <summary>
        /// Gets the Identity
        /// </summary>
        public Item IdentitySourceItem
        {
            get
            {
                Item identitySourceItem = null;

                try
                {
                    string identitySource = InnerItem.Fields[References.Templates.TopMenuSource.Fields.IdentitySource].Value;

                    if (!string.IsNullOrEmpty(identitySource))
                    {
                        identitySourceItem = SitecoreUtility.GetItem(new ID(identitySource));
                        
                    }
                }
                catch (Exception ex)
                {
                    LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
                }

                return identitySourceItem;
            }
        }

    }
}