using Sitecore.Mvc.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderForm;

namespace NISSitecore.Feature.PageContent.Forms.Pipeline
{
    public class SetFormAction : MvcPipelineProcessor<RenderFormEventArgs>
    {
		public override void Process(RenderFormEventArgs args)
		{
			var customAction = string.Empty;
			// Try get customAction from form submission
			if (args.IsPost)
			{
				var fullKey =
					HttpContext.Current.Request.Form.AllKeys.FirstOrDefault(key =>
						key.ToLower().EndsWith("customAction".ToLower()));
				if (!string.IsNullOrEmpty(fullKey))
					customAction = HttpContext.Current.Request.Form[fullKey];
			}
			if (string.IsNullOrEmpty(customAction))
			{
				// Get RenderingContext 
				var renderingContext = Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull;
				if (renderingContext == null && !args.Attributes.ContainsKey("customAction"))
					return;
				if (renderingContext != null && !renderingContext.Rendering.Parameters.Contains("action"))
					return;
				// Get customAction Form action from RenderingContext or Attributes
				customAction = renderingContext != null
					? renderingContext.Rendering.Parameters["action"]
					: (string)args.Attributes["customAction"];
			}
			if (string.IsNullOrEmpty(customAction))
				return;
			// Add CustomId to Attributes if not already there
			//if (!args.Attributes.ContainsKey("customAction"))
			//	args.Attributes.Add("customAction", customAction);
			// Setup custom form ID
			//args.FormHtmlId = customId;
			if (args.Attributes.ContainsKey("action"))
				args.Attributes["action"] = customAction;
			else
				args.Attributes.Add("action", customAction);
		}
	}
}