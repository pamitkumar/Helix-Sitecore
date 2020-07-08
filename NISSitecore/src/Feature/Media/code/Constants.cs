using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Media
{
    public static class Constants
    {
        public static class Carousel
        {
            public static readonly Guid TemplateId = new Guid("{2C34213E-1994-41F5-BED5-79025A55093D}");
        }
        public static class CarouselSlide
        {
            public static readonly Guid TemplateId = new Guid("{D79CA584-55CD-43ED-90A4-1BE165E515A0}");
        }

        public static class DefaultBoxContent
        {
            public static readonly Guid TemplateId = new Guid("{3D36CCA7-3DFD-4D08-BE83-C7405B0B5D8B}");
            public static class Fields
            {
                public static readonly ID Title = new ID("{50923BA6-A05C-4AC7-A5EA-C81F8513B98A}");
                public static readonly ID Content = new ID("{D9D94F91-DD84-49F4-BFF1-AE557FA172B1}");
                public static readonly ID Image = new ID("{A2A5161F-E914-49AF-9B3B-C0D70C48EDD1}");
            }
        }

        public static class Logging
        {
            public static class Error
            {
                public const string DataSourceError = "The Hero datasource was empty";
            }
        }

        public static class MediatorCodes
        {
            public static class MediaResponse
            {
                public const string DataSourceError = "MediaMediator.CreateMediaViewModel.DataSourceError";
                public const string ViewModelError = "MediaMediator.CreateMediaViewModel.ViewModelError";
                public const string Ok = "MediaMediator.CreateMediaViewModel.Ok";
            }
        }
    }
}