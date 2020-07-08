using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NISSitecore.Foundation.ORM.Models
{
    public interface IGlassBase
    {
        Guid Id { get; set; }
        Language Language { get; set; }
        int Version { get; set; }
        IEnumerable BaseTemplateIds { get; set; }
        string TemplateName { get; set; }
        Guid TemplateId { get; set; }
        string Name { get; set; }
        [SitecoreChildren(InferType = true)]
        IEnumerable<IGlassBase> BaseChildren { get; set; }
        string DisplayName { get; }
        string Url { get; }
    }
}
