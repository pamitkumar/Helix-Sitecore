using Glass.Mapper.Sc;
using NISSitecore.Feature.Media.Models;
using NISSitecore.Feature.Media.Services;
using NISSitecore.Feature.Media.ViewModels;
using NISSitecore.Foundation.Core.Models;
using NISSitecore.Foundation.Core.Services;
using System.Web;
using static NISSitecore.Feature.Media.Constants;

namespace NISSitecore.Feature.Media.Mediators
{
    public class CarouselMediator : ICarouselMediator
    {
        private readonly IMediatorService _mediatorService;
        private readonly ICarouselService _carouselService;
        private readonly IGlassHtml _glassHtml;
        public CarouselMediator(IMediatorService mediatorService, ICarouselService carouselService, IGlassHtml glassHtml)
        {
            _mediatorService = mediatorService;
            _carouselService = carouselService;
            _glassHtml = glassHtml;
        }
        public MediatorResponse<CarouselViewModel> RequestCarouselViewModel()
        {
            var carouselItemDataSource = _carouselService.GetCarouselItems();

            if (carouselItemDataSource == null)
                return _mediatorService.GetMediatorResponse<CarouselViewModel>(MediatorCodes.MediaResponse.DataSourceError);

            var viewModel = CreateCarouselViewModel(carouselItemDataSource, _carouselService.IsExperienceEditor);

            if (viewModel == null)
                return _mediatorService.GetMediatorResponse<CarouselViewModel>(MediatorCodes.MediaResponse.ViewModelError);

            return _mediatorService.GetMediatorResponse<CarouselViewModel>(MediatorCodes.MediaResponse.Ok, viewModel);
        }

        public CarouselViewModel CreateCarouselViewModel(ICarousel carouselItemDataSource, bool isExperienceEditor)
        {
            return new CarouselViewModel
            {
                Intro = new HtmlString(_glassHtml.Editable(carouselItemDataSource, i => i.Intro)),
                CarouselTitle=new HtmlString(_glassHtml.Editable(carouselItemDataSource, i=> i.Title)),
                CarouselCaption=new HtmlString(_glassHtml.Editable(carouselItemDataSource, i=>i.Caption)),
                AdditionalText=new HtmlString(_glassHtml.Editable(carouselItemDataSource,i=>i.AdditionalText)),
                Slides=carouselItemDataSource.Images,
                IsExperienceEditor= isExperienceEditor
            };
        }

    }
}