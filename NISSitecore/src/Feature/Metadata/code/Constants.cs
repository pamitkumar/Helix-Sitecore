using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Metadata
{
    public static class Constants
    {
        public static class HomePage
        {
            public static readonly Guid TemplateId = new Guid("{4A8822CC-B418-4452-AB0F-8BB6DF2F2114}");
        }

        public static class Keyword
        {
            public static readonly Guid TemplateId = new Guid("{409F883A-0DC8-431A-9508-7316B59B92BE}");
        }
        public static class PageMetadata
        {
            public static readonly Guid TemplateId = new Guid("{D88CCD80-D851-470D-AF11-701FF23504E7}");
        }

        public static class SiteMetadata
        {
            public static readonly Guid TemplateId = new Guid("{CF38E914-9298-47CC-9205-210553E79F97}");
        }

        public static class MetaKeyword
        {
            public static readonly Guid TemplateId = new Guid("{18CDD4CE-CDBE-4BDC-9D5A-6249F7F0EC17}");
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