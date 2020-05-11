using NISSitecore.Feature.Media.ViewModels;
using NISSitecore.Foundation.Core.Models;

namespace NISSitecore.Feature.Media.Mediators
{
    public interface ICarouselMediator
    {
        MediatorResponse<CarouselViewModel> RequestCarouselViewModel();
    }
}
