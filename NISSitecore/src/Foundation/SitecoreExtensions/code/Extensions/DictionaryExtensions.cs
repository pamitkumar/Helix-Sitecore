
using Newtonsoft.Json;
using Sitecore;
using Sitecore.Collections;
using Sitecore.StringExtensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace NISSitecore.Foundation.SitecoreExtensions.Extensions
{
    public static class DictionaryExtensions
    {
        //public static string ToJsonDictionary(this Dictionary<string, string> pairs)
        //{
        //    return pairs.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>)(p => "'" + p.Key + "':\"" + p.Value + "\"")).Aggregate(",")
        //}

        public static SafeDictionary<string> ToSafeDictionary(
          this NameValueCollection collection)
        {
            SafeDictionary<string> safeDictionary = new SafeDictionary<string>();
            foreach (string key in collection.Keys)
            {
                if (key != null)
                    safeDictionary.Add(key, collection[key]);
            }
            return safeDictionary;
        }

        public static SafeDictionary<string> AddParameter(
          this SafeDictionary<string> dictionary,
          string key,
          string value)
        {
            return dictionary.AddParameters((IDictionary<string, string>)new Dictionary<string, string>()
      {
        {
          key,
          value
        }
      });
        }

        public static SafeDictionary<string> AddParameters(
          this SafeDictionary<string> dictionary,
          IDictionary<string, string> add)
        {
            foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>)add)
                dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            return dictionary;
        }

        public static int GetIntValue(
          this Dictionary<string, string> dictionary,
          string name,
          int defaultValue = 0)
        {
            return MainUtil.GetInt(dictionary.ContainsKey(name) ? dictionary[name] : "0", defaultValue);
        }

        public static bool GetBooleanValue(this Dictionary<string, string> dictionary, string key)
        {
            return MainUtil.GetBool(dictionary.ContainsKey(key) ? dictionary[key] : "0", false);
        }

        public static bool GetBooleanValue(
          this IEnumerable<KeyValuePair<string, string>> dictionary,
          string key,
          bool defaultValue)
        {
            KeyValuePair<string, string> keyValuePair = dictionary.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(pair => pair.Key.Is(key)));
            return keyValuePair.Value.IsNullOrEmpty() ? defaultValue : string.Equals(keyValuePair.Value, "1", StringComparison.OrdinalIgnoreCase);
        }

        public static string GetValue<TKey>(this Dictionary<TKey, string> dictionary, TKey key)
        {
            return dictionary.GetValue<TKey, string>(key) ?? string.Empty;
        }

        public static T GetValue<TKey, T>(this Dictionary<TKey, T> dictionary, TKey key)
        {
            return !dictionary.ContainsKey(key) ? default(T) : dictionary[key];
        }

        public static Dictionary<string, IList<string>> AddAttribute(
          this Dictionary<string, IList<string>> attributes,
          string attributeName,
          string attributeValue)
        {
            if (attributes.ContainsKey(attributeName))
                attributes[attributeName].Add(attributeValue);
            else
                attributes.Add(attributeName, (IList<string>)new List<string>()
        {
          attributeValue
        });
            return attributes;
        }

        public static Dictionary<string, IList<string>> AddAttribute(
          this Dictionary<string, IList<string>> attributes,
          string attributeName,
          IList<string> attributeValues)
        {
            if (attributes.ContainsKey(attributeName))
                attributes[attributeName] = (IList<string>)attributes[attributeName].Concat<string>((IEnumerable<string>)attributeValues).ToList<string>();
            else
                attributes.Add(attributeName, attributeValues);
            return attributes;
        }

        public static string SerializeCustomData(this SafeDictionary<string, object> customData)
        {
            object obj = (object)null;
            if (customData != null)
                obj = (object)customData.ToArray<KeyValuePair<string, object>>();
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            }).Base64Encode();
        }
    }
}