using Sitecore.Web;
using System;

namespace NISSitecore.Foundation.Abstractions
{
    public class PageMode:IPageMode
    {
        public bool IsDebugging
        {
            get
            {
                return Sitecore.Context.PageMode.IsDebugging;
            }
        }

        public bool IsExperienceEditor
        {
            get
            {
                return Sitecore.Context.Site != null && Sitecore.Context.PageMode.IsExperienceEditor;
            }
        }

        public bool IsExperienceEditorEditing
        {
            get
            {
                return Sitecore.Context.Site != null && Sitecore.Context.PageMode.IsExperienceEditorEditing;
            }
        }

        public bool IsNormal
        {
            get
            {
                return Sitecore.Context.Site != null && Sitecore.Context.PageMode.IsNormal;
            }
        }

        public bool IsPreview
        {
            get
            {
                return Sitecore.Context.Site != null && Sitecore.Context.PageMode.IsPreview;
            }
        }

        public bool IsSimulatedDevicePreviewing
        {
            get
            {
                return Sitecore.Context.PageMode.IsSimulatedDevicePreviewing;
            }
        }

        public bool IsProfiling
        {
            get
            {
                return Sitecore.Context.PageMode.IsProfiling;
            }
        }

        public bool IsHorizonEditing
        {
            get
            {
                return string.Equals(WebUtil.GetQueryString("sc_horizon", string.Empty, Sitecore.Context.HttpContext), "true", StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}