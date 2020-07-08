using Microsoft.Extensions.DependencyInjection;
using NISSitecore.Foundation.Abstractions;
using NISSitecore.Foundation.Abstractions.Services;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Mvc;
using Sitecore.Mvc.Helpers;
using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace NISSitecore.Foundation.SitecoreExtensions.Extensions
{
    public class RazorExtensions
    {
        private readonly HtmlHelper _htmlHelper;

        protected ITrackingService TrackingService { get; set; } = (ITrackingService)ServiceLocator.ServiceProvider.GetService(typeof(ITrackingService));

        protected IPageMode PageMode { get; } = (IPageMode)ServiceLocator.ServiceProvider.GetService(typeof(IPageMode));

        public RazorExtensions(HtmlHelper htmlHelper)
        {
            this._htmlHelper = htmlHelper;
        }

        public IHtmlString Field(string fieldName, Item item, bool disableWebEditing)
        {
            SitecoreHelper sitecoreHelper = new SitecoreHelper(this._htmlHelper);
            var data1 = this.PageMode.IsNormal ? null : new
            {
                DisableWebEdit = disableWebEditing
            };
            string fieldName1 = fieldName;
            Item obj = item;
            var data2 = data1;
            return (IHtmlString)sitecoreHelper.Field(fieldName1, obj, (object)data2);
        }

        public IHtmlString Field(string fieldName, Item item, object parameters)
        {
            return (IHtmlString)new SitecoreHelper(this._htmlHelper).Field(fieldName, item, parameters);
        }

        public HtmlString Field(ID fieldId)
        {
            return new SitecoreHelper(this._htmlHelper).Field(fieldId.ToString());
        }

        public HtmlString Field(ID fieldId, object parameters)
        {
            return new SitecoreHelper(this._htmlHelper).Field(fieldId.ToString(), parameters);
        }

        public HtmlString Field(ID fieldId, Item item, object parameters)
        {
            return new SitecoreHelper(this._htmlHelper).Field(fieldId.ToString(), item, parameters);
        }

        public HtmlString Field(ID fieldId, Item item)
        {
            return new SitecoreHelper(this._htmlHelper).Field(fieldId.ToString(), item);
        }

        public IHtmlString RenderHeading(
          string wrappingTag,
          Item item,
          string headingFieldName,
          object parameters)
        {
            HtmlString htmlString = new SitecoreHelper(this._htmlHelper).Field(headingFieldName, item, parameters);
            return !this.PageMode.IsExperienceEditorEditing && htmlString.ToHtmlString().IsNullOrWhiteSpace() ? (IHtmlString)htmlString : (IHtmlString)new HtmlString(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", (object)wrappingTag, (object)htmlString));
        }

        public bool IsEdit
        {
            get
            {
                return this.PageMode.IsExperienceEditorEditing;
            }
        }

        public bool IsPreview
        {
            get
            {
                return this.PageMode.IsPreview;
            }
        }        
        public IHtmlString RenderRaw(string str)
        {
            return (IHtmlString)new HtmlString("<pre>" + WebUtility.HtmlEncode(str) + "</pre>");
        }

        public virtual HtmlString Placeholder(string placeholderName)
        {
            HtmlString htmlString = this._htmlHelper.Sitecore().Placeholder(placeholderName);
            return !this.PageMode.IsExperienceEditorEditing ? htmlString : new HtmlString(new Regex("<code(.)+>(.)+<\\/code>", RegexOptions.Compiled).Replace(htmlString.ToString(), ""));
        }

        public virtual HtmlString VisitorIdentification()
        {
            return this.TrackingService != null ? this.TrackingService.GetVisitorIdentification() : new HtmlString(string.Empty);
        }
    }
}