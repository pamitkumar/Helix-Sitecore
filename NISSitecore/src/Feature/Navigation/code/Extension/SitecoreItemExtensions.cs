using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace NISSitecore.Feature.Navigation.Extension
{
    public static class SitecoreItemExtensions
    {
        public static string GetStringValue(this Item item, string fieldName)
        {
            if (item != null && item.Fields[fieldName] != null &&
              !string.IsNullOrEmpty(item.Fields[fieldName].Value))
            {
                return item.Fields[fieldName].Value;
            }
            return string.Empty;
        }

        public static string GetDropLinkValue(this Item item, string fieldName, string droplinkFieldName)
        {
            if (item != null && fieldName != null && item.Fields[fieldName] != null)
            {
                InternalLinkField linkField = item.Fields[fieldName];
                if (linkField.TargetItem != null)
                {
                    return linkField.TargetItem.Fields[droplinkFieldName].Value;
                }
            }
            return string.Empty;
        }

        public static bool GetCheckBoxValueDefaultTrue(this Item item, string fieldName)
        {
            CheckboxField checkBox = item.Fields[fieldName];
            if (checkBox != null)
            {
                return checkBox.Checked;
            }
            return true;
        }
    }
}