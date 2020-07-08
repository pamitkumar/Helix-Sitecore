using NISSitecore.Foundation.Abstractions;
using Sitecore;
using Sitecore.DependencyInjection;
using Sitecore.Sites;
using Sitecore.Web;
using System;
using System.Web;

namespace NISSitecore.Foundation.SitecoreExtensions.Extensions
{
    public static class SiteinfoExtensions
    {
        public static string GetServerUrlElement(this SiteInfo siteInfo, bool alwaysIncludeServerUrl)
        {
            SiteContext site = (SiteContext)ServiceLocator.ServiceProvider.GetService(typeof(IContext));
            
            string str1 = site != null ? site.Name : string.Empty;
            string str2 = alwaysIncludeServerUrl ? WebUtil.GetServerUrl() : string.Empty;
            if (siteInfo == null || siteInfo.Name.Equals(str1, StringComparison.OrdinalIgnoreCase))
                return str2;
            string hostName = WebUtil.GetHostName();
            string first1 = StringUtil.GetString(siteInfo.TargetHostName, siteInfo.IsHostNameUnique() ? siteInfo.HostName : (string)null, hostName);
            if (first1.Is(string.Empty) || first1.IndexOf('*') >= 0)
                return str2;
            string scheme = WebUtil.GetScheme();
            string first2 = StringUtil.GetString(siteInfo.Scheme, scheme, "http");
            if (first2.Is(string.Empty))
                return str2;
            int port = WebUtil.GetPort();
            int num = MainUtil.GetInt(siteInfo.Port, port);
            if (first1.Equals(hostName, StringComparison.OrdinalIgnoreCase) && num == port && first2.Equals(scheme, StringComparison.OrdinalIgnoreCase))
                return str2;
            string str3 = first2 + "://" + first1;
            if (num > 0 && num != 80)
                str3 = str3 + ":" + num.ToString();
            return str3;
        }

        public static bool IsHostNameUnique(this SiteInfo siteInfo)
        {
            return siteInfo.HostName.IndexOfAny(new char[2]
            {
        '*',
        '|'
            }) < 0;
        }

        public static string ResolveTargetHostName(this SiteInfo currentSite)
        {
            if (!string.IsNullOrEmpty(currentSite.TargetHostName))
                return currentSite.TargetHostName;
            return currentSite.IsHostNameUnique() ? currentSite.HostName : HttpContext.Current?.Request.Url.Host ?? "";
        }
    }
}