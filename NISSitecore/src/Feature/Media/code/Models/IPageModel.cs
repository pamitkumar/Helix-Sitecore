using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Fields;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Media.Models
{
    public interface IPageModel:IMediaGlassBase
    {
        //[SitecoreField(FieldId ="{50923BA6-A05C-4AC7-A5EA-C81F8513B98A}")]
        //string Title { get; set; }

        //[SitecoreField(FieldId = "{D9D94F91-DD84-49F4-BFF1-AE557FA172B1}")]
        //string Content { get; set; }

        //[SitecoreField(FieldId = "{A2A5161F-E914-49AF-9B3B-C0D70C48EDD1}")]
        //Image Image { get; set; }

        string Title { get; set; }

        string Content { get; set; }

        Image Image { get; set; }
    }
}