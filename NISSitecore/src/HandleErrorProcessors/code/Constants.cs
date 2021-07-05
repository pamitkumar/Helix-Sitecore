using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace Sitecon.Foundation.HandleErrorProcessors
{
  public struct Constants
  {
    public struct SiteInfo
    {
      public struct Properties
      {
        public static string EnableHandleCustomErrors = "enableHandleCustomErrors";
        public static string NotFoundItem = "notFoundItem";
        public static string FallbackNotFoundItem = "fallbackNotFoundItem";
        public static string CustomErrorFile = "customErrorFile";
      }
    }
  }
}