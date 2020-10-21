using Sitecore.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Web;
using Sitecore.ExperienceForms.Models;

namespace NISSitecore.Feature.PageContent.Forms.Uitlity
{
    public class Helper
    {
        public static IViewModel GetFieldByName(string name, IList<IViewModel> fields)
        {
            return fields.FirstOrDefault(f => f.Name == name);
        }

        private static string GetValue(object field)
        {
            return field?.GetType().GetProperty("Value")?.GetValue(field, null)?.ToString() ?? string.Empty;
        }
        public static string GetValue(IViewModel postedField)
        {
            Assert.ArgumentNotNull((object)postedField, "postedField");
            var valueField = postedField as IValueField;
            var property = postedField.GetType().GetProperty("Value");
            object obj;
            if (property == null)
            {
                obj = (object)null;
            }
            else
            {
                IViewModel viewModel = postedField;
                obj = property.GetValue((object)viewModel);
            }
            var postedValue = obj;
            if (postedValue == null)
            {
                return string.Empty;
            }
            var parsedValue = ParseFieldValue(postedValue);

            return parsedValue;
        }

        public static string ParseFieldValue(object postedValue)
        {
            Assert.ArgumentNotNull(postedValue, "postedValue");
            var list = new List<string>();
            if (postedValue is IEnumerable<object> secondList)
            {
                foreach (object obj in secondList)
                {
                    list.Add(obj.ToString());
                }
            }
            else
            {
                list.Add(postedValue.ToString());
            }
            return string.Join(",", list);
        }

        //public static IXdbContext CreateClient()
        //{
        //    return SitecoreXConnectClientConfiguration.GetClient();
        //}
    }
}