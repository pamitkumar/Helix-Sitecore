using NISSitecore.Feature.SocialShare.Models;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.SocialShare.Repository
{
    public class IDBSocialShare : ISocialShare
    {
        public Database ContextDb = Context.Database;
        public List<SocialItemProperty> GetSocialShareButtonList(string rootItemId)
        {
            List<SocialItemProperty> socialItemPropertyList = new List<SocialItemProperty>();
            try
            {
                Item obj = this.ContextDb.GetItem(rootItemId);
                if (obj != null)
                {
                    foreach (Item child in obj.GetChildren())
                    {
                        SocialItemProperty socialItemProperty = new SocialItemProperty();
                        socialItemProperty.SocialItem = child;
                        socialItemProperty.Img = GetImageURL(child);
                        if (child.Fields["Title"] != null)
                            socialItemProperty.Title = child.Fields["Title"].ToString();
                        if (child.Fields["url"] != null)
                            socialItemProperty.Url = child.Fields["url"].ToString();
                        if (child.Fields["Hide This Button"] != null)
                        {
                            CheckboxField field = (CheckboxField)child.Fields["Hide This Button"];
                            socialItemProperty.IsHideButtonChecked = field;
                        }
                        socialItemPropertyList.Add(socialItemProperty);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Error("EDComponents > EDSocialShare Exception", ex, (object)this);
            }
            return socialItemPropertyList;
        }

        public static string GetImageURL(Item currentItem)
        {
            string str = string.Empty;
            ImageField field = (ImageField)currentItem.Fields["Image"];
            if (field != null && field.MediaItem != null)
                str = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(new MediaItem(field.MediaItem)));
            return str;
        }
    }
}