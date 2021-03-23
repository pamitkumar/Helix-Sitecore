
namespace Corlate.Feature.ProjectConfigurations.Models
{
    using Corlate.Foundation.Common.Utilities;
    using Sitecore.Data.Items;
    using System;

    
    public class Identity : CustomItem
    {
        public Identity(Item innerItem) : base(innerItem)
        {
        }

        /// <summary>
        /// Gets the LogoTitle
        /// </summary>
        public string LogoTitle
        {
            get
            {
                return InnerItem.Fields[References.Templates.Identity.Fields.LogoTitle].Value;
            }
        }

        /// <summary>
        /// Gets the LogoTargetURL
        /// </summary>
        public string LogoTargetURL
        {
            get
            {
                return SitecoreUtility.GetLinkURL(InnerItem, References.Templates.Identity.Fields.LogoTargetURL);
            }
        }

        /// <summary>
        /// Gets the FooterCopyrightTextID
        /// </summary>
        public string FooterCopyrightTextID
        {
            get
            {
                return References.Templates.Identity.Fields.FooterCopyrightText.ToString();
            }
        }

        /// <summary>
        /// Gets the FaviconURL
        /// </summary>
        public string FaviconURL
        {
            get
            {
                try
                {
                    return SitecoreUtility.GetMediaItemURL(InnerItem, References.Templates.Identity.Fields.Favicon);
                }
                catch (Exception ex)
                {
                    LogManager.SaveLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex, LogManager.LogTypes.Error, string.Empty);
                    return string.Empty;
                }
            }
        }
    }
}
