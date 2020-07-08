using Sitecore.Abstractions;
using Sitecore.Access;
using Sitecore.Caching;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Layouts;
using Sitecore.Resources;
using Sitecore.Security.Accounts;
using Sitecore.Security.Domains;
using Sitecore.Sites;
using Sitecore.Tasks;
using Sitecore.Web.UI.Sheer;
using Sitecore.Workflows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;

namespace NISSitecore.Foundation.Abstractions
{
    public interface IContext
    {
        AccessContext Access { get; }

        ClientDataStore ClientData { get; }

        HttpContextBase HttpContext { get; }

        ClientPage ClientPage { get; set; }

        Database ContentDatabase { get; }

        Language ContentLanguage { get; }

        CultureInfo Culture { get; }

        Sitecore.Context.ContextData Data { get; }

        Database Database { get; set; }

        DeviceItem Device { get; set; }

        DiagnosticContext Diagnostics { get; }

        Domain Domain { get; }

        bool IsAdministrator { get; }

        bool IsBackgroundThread { get; }

        bool IsLoggedIn { get; }

        bool IsUnitTesting { get; set; }

        bool SkipSecurityInUnitTests { get; set; }

        Item Item { get; set; }

        ItemsContext Items { get; }

        BaseJob Job { get; set; }

        Language Language { get; set; }

        Sitecore.Events.NotificationContext Notifications { get; }

        PageContext Page { get; }

        string RawUrl { get; }

        SiteRequest Request { get; }

        string RequestID { get; }

        ResourceContext Resources { get; }

        SiteContext Site { get; set; }

        StateContext State { get; }

        TaskContext Task { get; }

        User User { get; }

        WorkflowContext Workflow { get; }

        string PreviewSiteName { get; set; }

        string GetDeviceName();

        string GetSiteName();

        Stack<TaskContext> GetTaskStack();

        string GetUserName();

        void SetActiveSite(string siteName);

        void SetLanguage(Language language, bool persistent);

        void SetLanguage(Language language, bool persistent, SiteContext site, DateTime? expiryDate = null);

        void SetTraceBuffer(StringBuilder buffer);

        void Trace(string message, params object[] parameters);
    }
}