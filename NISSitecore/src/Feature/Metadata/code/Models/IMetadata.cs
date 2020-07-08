using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Metadata.Models
{
   public interface IMetadata:IMetadataGlassBase
    {        
        IKeyword MetaKeyword { get; set; }
        IPageMetadata PageMetaData { get; set; }
        ISiteMetadata SiteMetadata { get; set; }
    }
}
