using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.PageContent
{
    public static class Constants
    {
        public static class HasPageContent
        {
            public static readonly ID TemplateId = new ID("{AF74A00B-8CA7-4C9A-A5C1-156A68590EE2}");
            public static class Fields
            {
                public static readonly ID Title = new ID("{C30A013F-3CC8-4961-9837-1C483277084A}");
               
                public static readonly ID Summary = new ID("{AC3FD4DB-8266-476D-9635-67814D91E901}");
               
                public static readonly ID Body = new ID("{D74F396D-5C5E-4916-BD0A-BFD58B6B1967}");
               
                public static readonly ID Image = new ID("{9492E0BB-9DF9-46E7-8188-EC795C4ADE44}");
            }
        }

        public static class Renderings
        {
            public static readonly ID PageTeaser = new ID("{23F25A17-3393-4CBB-A9FC-DD786A5F9802}");
        }

    }
}