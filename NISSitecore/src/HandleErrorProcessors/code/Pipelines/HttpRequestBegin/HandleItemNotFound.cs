﻿using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using Sitecon.Foundation.HandleErrorProcessors.Services;

namespace Sitecon.Foundation.HandleErrorProcessors.Pipelines.HttpRequestBegin
{
  public class HandleItemNotFound : HttpRequestProcessor
  {
    public List<string> RelativeUrlPrefixesToIgnore { get; set; }

    public HandleItemNotFound()
    {
      RelativeUrlPrefixesToIgnore = new List<string>();
    }

    public override void Process(HttpRequestArgs args)
    {
      var enableHandleCustomErrors = MainUtil.GetBool(Context.Site.Properties[Constants.SiteInfo.Properties.EnableHandleCustomErrors], false);
      if (!enableHandleCustomErrors)
      {
        return;
      }

      if (Context.Item != null || StartsWithPrefixToIgnore(args.Url.FilePath))
      {
        return;
      }

      var notFoundContent = GetNotFoundPageContent(args);
      if (!string.IsNullOrWhiteSpace(notFoundContent))
      {
        
        args.HttpContext.Response.TrySkipIisCustomErrors = true;
        args.HttpContext.Response.StatusCode = 404;
        args.HttpContext.Response.StatusDescription = "Page Not Found";
        args.HttpContext.Response.Write(notFoundContent);
        args.HttpContext.Response.End();
      }

      Log.Warn("The not found page: '{0}' shows no content when rendered!", args.Url);
    }

    protected bool StartsWithPrefixToIgnore(string url)
    {
      return !string.IsNullOrWhiteSpace(url) && RelativeUrlPrefixesToIgnore.Any(prefix => url.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }

    protected string GetNotFoundPageContent(HttpRequestArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      var domain = GetDomain(args);

      var notFoundItem = NotFoundItemService.GetItemBySiteProperty(Context.Site, Constants.SiteInfo.Properties.NotFoundItem);
      var url = notFoundItem != null ? LinkManager.GetItemUrl(notFoundItem) : NotFoundItemService.GetRelativeFilePathBySiteProperty(Context.Site, Constants.SiteInfo.Properties.FallbackNotFoundItem);
      if (string.IsNullOrWhiteSpace(url))
      {
        return string.Empty;
      }

      try
      {
        var content = WebUtil.ExecuteWebPage(string.Concat(domain, url));
        return content;
      }
      catch (Exception ex)
      {
        Log.Error($"{ToString()} Error - domain: {domain}, url: {url}", ex, this);
      }

      return string.Empty;
    }

    protected string GetDomain(HttpRequestArgs args)
    {
      Assert.ArgumentNotNull(args, "args");
      return args.HttpContext.Request.Url?.GetComponents(UriComponents.Scheme | UriComponents.Host, UriFormat.Unescaped);
    }
  }
}
