using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Metadata.Models
{
    public interface IMetaKeyword:IMetadataGlassBase
    {
        IKeyword Keyword { get; set; }
    }
}
