using NISSitecore.Feature.Metadata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Metadata.Services
{
    public interface IMetadataService
    {
        IPageMetadata GetPageMetadataItems();

        ISiteMetadata GetSiteMetadataItems();
        bool IsExperienceEditor { get; }
    }
}
