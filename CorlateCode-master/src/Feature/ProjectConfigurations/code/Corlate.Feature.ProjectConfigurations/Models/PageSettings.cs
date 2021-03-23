
namespace Corlate.Feature.ProjectConfigurations.Models
{
    using Corlate.Foundation.Common.Utilities;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using System;

    public class PageSettings : CustomItem
    {
        public PageSettings(Item innerItem) : base(innerItem)
        {
        }

        /// <summary>
        /// Gets the Identity
        /// </summary>
        public Identity Identity
        {
            get
            {
                Identity identity = null;

                try
                {
                    string identitySource = InnerItem.Fields[References.Templates.PageSettings.Fields.IdentitySource].Value;

                    if (!string.IsNullOrEmpty(identitySource))
                    {
                        Item identityItem = SitecoreUtility.GetItem(new ID(identitySource));
                        identity = identityItem != null && identityItem.TemplateID == References.Templates.Identity.ID ? new Identity(identityItem) : null;
                    }
                }
                catch (Exception ex)
                {
                    LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
                    identity = null;
                }

                return identity;
            }
        }
    }
}
