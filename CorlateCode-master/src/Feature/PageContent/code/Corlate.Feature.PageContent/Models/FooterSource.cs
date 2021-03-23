
namespace Corlate.Feature.PageContent.Models
{
    using Corlate.Feature.PageContent;
    using Foundation.Common.Utilities;
    using ProjectConfigurations.Models;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using System;
    using System.Collections.Generic;

    public class FooterSource : CustomItem
    {
        public FooterSource(Item innerItem) : base(innerItem) { }

        public string TopSectionTitleID
        {
            get
            {
                return References.Templates.FooterSource.Fields.TopSectionTitle.ToString();
            }
        }

        public List<FooterSocialLink> SocialLinks { get; set; }


        public Identity Identity { get; set; }

        public bool IsActive
        {
            get
            {
                return InnerItem.Fields[References.Templates.FooterSource.Fields.IsActive].Value == "1";
            }
        }

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
                    string identitySource = InnerItem.Fields[References.Templates.FooterSource.Fields.IdentitySource].Value;

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
