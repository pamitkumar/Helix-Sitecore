using Sitecore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace NISSitecore.Foundation.SitecoreExtensions.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static string ToQueryString(this NameValueCollection nvc)
        {
            return nvc.Count == 0 ? string.Empty : ((IEnumerable<string>)nvc.AllKeys).Select<string, string>((Func<string, string>)(key => HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(nvc[key]))).Aggregate<string>((Func<string, string, string>)((a, b) => a + "&" + b));
        }

        public static NameValueCollection EscapeDataValues(
          this NameValueCollection nvc)
        {
            foreach (string allKey in nvc.AllKeys)
                nvc[allKey] = Uri.EscapeDataString(nvc[allKey]);
            return nvc;
        }

        public static void AddIfNotExist(this NameValueCollection nvc, string key, string value)
        {
            if (((IEnumerable<string>)nvc.AllKeys).Contains<string>(key))
                return;
            nvc.Add(key, value);
        }

        public static int ParseInt(this NameValueCollection nvc, string name, int defaultValue = 0)
        {
            return MainUtil.GetInt(nvc[name], defaultValue);
        }

        public static Dictionary<string, string> ToDictionary(this NameValueCollection nvc)
        {
            return ((IEnumerable<string>)nvc.AllKeys).ToDictionary<string, string, string>((Func<string, string>)(k => k), (Func<string, string>)(k => nvc[k]));
        }

        public static void AddIfNotExist<T>(this Dictionary<string, T> dictionary, string key, T value)
        {
            if (dictionary.ContainsKey(key))
                return;
            dictionary.Add(key, value);
        }

        public static void Merge(this NameValueCollection a, NameValueCollection b)
        {
            foreach (string key in b.Keys)
            {
                if (!((IEnumerable<string>)a.AllKeys).Contains<string>(key))
                    a.Add(key, b[key]);
            }
        }
    }
}