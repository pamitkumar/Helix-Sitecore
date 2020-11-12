using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NISSitecore.Feature.SocialShare.Repository;
using Sitecore.Configuration;

namespace NISSitecore.Feature.SocialShare.Models
{
    public class SocialButton : IRenderingModel
    {
        //private readonly ISocialShare socialShareRepository;
        public List<SocialMediaProperty> SocialMediaList = new List<SocialMediaProperty>();

        //public SocialButton(ISocialShare socialShare)
        //{
        //    socialShareRepository = socialShare;
        //}
        public void Initialize(Rendering rendering)
        {
            //new EDLog().ErrorIs();
            Item obj = Context.Item;
            string str1 = HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString().Replace("{", "").Replace("}", ""));
            string str2 = string.Empty;
            string str3 = string.Empty;
            List<SocialItemProperty> socialItemPropertyList = new List<SocialItemProperty>();
            if (obj.Fields["BrowserTitle"] != null)
                str2 = HttpUtility.UrlEncode(obj.Fields["BrowserTitle"].GetValue(true));
            if (obj.Fields["MetaDescription"] != null)
                str3 = HttpUtility.UrlEncode(obj.Fields["MetaDescription"].GetValue(true));
            Language language = PageContext.Current.Item.Language;
           
            foreach (SocialItemProperty socialItemProperty in  new SocialShare.Repository.IDBSocialShare().GetSocialShareButtonList(Settings.GetSetting("SocialMediaNodeID")).Where<SocialItemProperty>((Func<SocialItemProperty, bool>)(x => x.IsHideButtonChecked != null && !x.IsHideButtonChecked.Checked)).ToList<SocialItemProperty>())
            {
                SocialMediaProperty socialMediaProperty = new SocialMediaProperty();
                if (socialItemProperty.SocialItem.ID.ToString() == "{4BAE127B-0E73-4C80-AEBF-35B62DE3DC3B}") //LinkedIn
                {
                    socialMediaProperty.SocialMediaUrl = socialItemProperty.Url + str1 + "&amp;title=" + str2 + "&amp;summary=" + str3 + "&amp;source=Component";
                    socialMediaProperty.SocialMediaImage = socialItemProperty.Img;
                    socialMediaProperty.SocialMediaTitle = socialItemProperty.Title;
                }
                if (socialItemProperty.SocialItem.ID.ToString() == "{F4002B19-9ECB-42D8-900D-21F003CA3CA4}") //Twitter
                {
                    socialMediaProperty.SocialMediaUrl = socialItemProperty.Url + str1 + "&amp;text=" + str2 + "&amp;lang=" + (object)language;
                    socialMediaProperty.SocialMediaImage = socialItemProperty.Img;
                    socialMediaProperty.SocialMediaTitle = socialItemProperty.Title;
                }
                if (socialItemProperty.SocialItem.ID.ToString() == "{85D07CF1-F8DA-41E1-BF9D-D62BEA4AEAC0}") //Facebook
                {
                    socialMediaProperty.SocialMediaUrl = socialItemProperty.Url + str1;
                    socialMediaProperty.SocialMediaImage = socialItemProperty.Img;
                    socialMediaProperty.SocialMediaTitle = socialItemProperty.Title;
                }
                if (socialItemProperty.SocialItem.ID.ToString() == "{08DBD74D-AE3E-4EC9-8A87-D57554A30540}") //Googleplus
                {
                    socialMediaProperty.SocialMediaUrl = socialItemProperty.Url + str1 + "&amp;hl=" + (object)language;
                    socialMediaProperty.SocialMediaImage = socialItemProperty.Img;
                    socialMediaProperty.SocialMediaTitle = socialItemProperty.Title;
                }
                if (socialItemProperty.SocialItem.ID.ToString() == "{3BC74D7A-BEF5-423B-83EF-811BAFC8D533}") //MailTo
                {
                    socialMediaProperty.SocialMediaUrl = socialItemProperty.Url + str1;
                    socialMediaProperty.SocialMediaImage = socialItemProperty.Img;
                    socialMediaProperty.SocialMediaTitle = socialItemProperty.Title;
                }
                this.SocialMediaList.Add(socialMediaProperty);
            }
        }
    }
}