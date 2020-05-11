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