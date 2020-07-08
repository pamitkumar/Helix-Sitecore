using System.Collections.Generic;
using System.Collections.Specialized;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace NISSitecore.Feature.Metadata.Models
{
    public interface IPageMetadata: IMetadataGlassBase
    {
        string BrowserTitle { get; set; }

        string MetaDescription { get; set; }
        
        IEnumerable<IMetaKeyword> MetaKeywords { get; set; }

        bool CanIndex { get; set; }

        bool SeoFollowLinks { get; set; }

        NameValueCollection CustomMetaData { get; set; }
    }

}
