using HtmlAgilityPack;
using Newtonsoft.Json;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace NISSitecore.Foundation.SitecoreExtensions.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveLineBreaks(this string lines)
        {
            return lines.ReplaceLineBreaks(string.Empty);
        }

        public static bool Is(this string first, string second)
        {
            return string.Equals(first, second, StringComparison.Ordinal);
        }

        public static string ReplaceLineBreaks(this string input, string replacement)
        {
            return input.Replace("\r\n", replacement).Replace("\r", replacement).Replace("\n", replacement);
        }

        public static string GetNormalizedName(this string name)
        {
            return name.ToLowerInvariant().Replace(' ', '-');
        }

        public static T ToEnum<T>(this string value)
        {
            return value.IsNullOrWhiteSpace() ? default(T) : (T)Enum.Parse(typeof(T), value);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static string RemoveWhitespace(this string input)
        {
            return new string(((IEnumerable<char>)input.ToCharArray()).Where<char>((Func<char, bool>)(c => !char.IsWhiteSpace(c))).ToArray<char>());
        }

        public static string EscapeUnicode(this string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char ch in value)
            {
                if (ch > '\x007F')
                    stringBuilder.Append("&#" + ((int)ch).ToString() + ";");
                else
                    stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }

        public static string ReplaceFirst(
          this string text,
          string search,
          string replace,
          StringComparison stringComparison = StringComparison.Ordinal)
        {
            int length = text.IndexOf(search, stringComparison);
            return length < 0 ? text : text.Substring(0, length) + replace + text.Substring(length + search.Length);
        }

        public static string EscapeQuery(this string query)
        {
            if (!query.Contains("-"))
                return query;
            string[] strArray = query.Split('/');
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index].IndexOf('-') > 0 && strArray[index].IndexOfAny("*[]()@'=#:".ToCharArray()) == -1)
                    strArray[index] = "#" + strArray[index] + "#";
            }
            string str = string.Join("/", strArray);
            return query.StartsWith("/", StringComparison.Ordinal) ? StringUtil.EnsurePrefix('/', str) : str;
        }

        public static bool IsSitecoreQuery(this string query)
        {
            Assert.ArgumentNotNull((object)query, nameof(query));
            return query.StartsWith("query:", StringComparison.OrdinalIgnoreCase);
        }

        public static string AppendNewLine(this string originalString, string newLineString)
        {
            return !string.IsNullOrWhiteSpace(originalString) ? originalString + Environment.NewLine + newLineString : newLineString;
        }

        public static string HtmlToPlainText(this string input)
        {
            return new StringExtensions.HtmlToText().ConvertHtml(input);
        }

        public static int ToInt(this string str, int defaultValue = 0)
        {
            return MainUtil.GetInt(str, defaultValue);
        }

        public static SafeDictionary<string, object> DeserializeCustomData(
          this string customData)
        {
            IEnumerable<KeyValuePair<string, object>> keyValuePairs = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<string, object>>>(customData.Base64Decode());
            SafeDictionary<string, object> safeDictionary = new SafeDictionary<string, object>();
            if (keyValuePairs != null)
            {
                foreach (KeyValuePair<string, object> keyValuePair in keyValuePairs)
                    safeDictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }
            return safeDictionary;
        }

        public static string MergeQueryString(this string link, string queryString, string anchor = "")
        {
            NameValueCollection queryString1 = HttpUtility.ParseQueryString(queryString);
            if (queryString1.Count == 0 && string.IsNullOrWhiteSpace(anchor))
                return link;
            int num = link.IndexOf('#');
            if (num <= -1)
                return link.MergeQueryString(queryString1, anchor);
            string link1 = link.Substring(0, num);
            string str = link.Substring(num);
            NameValueCollection queryString2 = queryString1;
            string anchor1 = str;
            return link1.MergeQueryString(queryString2, anchor1);
        }

        public static string MergeQueryString(
          this string link,
          NameValueCollection queryString,
          string anchor = "")
        {
            if (!string.IsNullOrWhiteSpace(anchor))
                anchor = StringUtil.EnsurePrefix('#', anchor);
            if (queryString.Count > 0)
                link = StringUtil.EnsurePostfix(link.Contains("?") ? '&' : '?', link);
            return FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}{2}", (object)link, (object)queryString, (object)anchor));
        }

        public static string Base64Encode(this string plainText)
        {
            return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            return Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));
        }

        public static bool EqualsId(this string idString, ID id)
        {
            ID result;
            return ID.TryParse(idString, out result) && result.Equals(id);
        }

        private class HtmlToText
        {
            public string ConvertHtml(string html)
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                StringWriter stringWriter = new StringWriter();
                this.ConvertTo(htmlDocument.DocumentNode, (TextWriter)stringWriter);
                stringWriter.Flush();
                return stringWriter.ToString();
            }

            private void ConvertContentTo(HtmlNode node, TextWriter outText)
            {
                foreach (HtmlNode childNode in (IEnumerable<HtmlNode>)node.ChildNodes)
                    this.ConvertTo(childNode, outText);
            }

            public void ConvertTo(HtmlNode node, TextWriter outText)
            {
                switch (node.NodeType)
                {
                    case HtmlNodeType.Document:
                        this.ConvertContentTo(node, outText);
                        break;
                    case HtmlNodeType.Element:
                        switch (node.Name)
                        {
                            case "p":
                                outText.Write("\r\n");
                                break;
                        }
                        if (!node.HasChildNodes)
                            break;
                        this.ConvertContentTo(node, outText);
                        break;
                    case HtmlNodeType.Text:
                        string name = node.ParentNode.Name;
                        if (name.Is("script") || name.Is("style"))
                            break;
                        string text = ((HtmlTextNode)node).Text;
                        if (HtmlNode.IsOverlappedClosingElement(text) || text.Trim().Length <= 0)
                            break;
                        outText.Write(HtmlEntity.DeEntitize(text));
                        break;
                }
            }
        }
    }
}