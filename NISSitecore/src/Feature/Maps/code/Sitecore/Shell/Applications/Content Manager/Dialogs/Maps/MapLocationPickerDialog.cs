using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using System;

namespace NISSitecore.Feature.Maps.Sitecore.Shell.Applications.Content_Manager.Dialogs.Maps
{

    public class MapLocationPickerDialog : DialogForm
    {
        #region Control

        public Edit TxtSelectedLocation;

        #endregion

        #region Control Event

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var value = WebUtil.GetQueryString("value");
            if (!string.IsNullOrWhiteSpace(value) && string.IsNullOrWhiteSpace(this.TxtSelectedLocation.Value))
            {
                this.TxtSelectedLocation.Value = value;
            }
        }

        protected override void OnOK(object sender, EventArgs args)
        {

            SheerResponse.SetDialogValue(this.TxtSelectedLocation.Value);
            base.OnOK(sender, args);
        }
        #endregion
    }
}