using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Navigation
{
    public static class Constants
    {
        public static class SitemapContent
        {
            public static readonly Guid TemplateId = new Guid("{3D36CCA7-3DFD-4D08-BE83-C7405B0B5D8B}");
            public static class Fields
            {
                public static readonly ID Title = new ID("{50923BA6-A05C-4AC7-A5EA-C81F8513B98A}");
                public static readonly ID Content = new ID("{D9D94F91-DD84-49F4-BFF1-AE557FA172B1}");
                public static readonly ID Image = new ID("{A2A5161F-E914-49AF-9B3B-C0D70C48EDD1}");
            }
        }

    }
}