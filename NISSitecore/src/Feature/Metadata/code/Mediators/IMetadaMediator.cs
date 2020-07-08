using NISSitecore.Feature.Metadata.ViewModels;
using NISSitecore.Foundation.Core.Models;

namespace NISSitecore.Feature.Metadata.Mediators
{
    public interface IMetadaMediator
    {
        MediatorResponse<MetadataViewModel> RequestMetadataViewModel();
    }
}
