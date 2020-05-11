using NISSitecore.Feature.Media.Mediators;
using NISSitecore.Foundation.Core.Exceptions;
using Sitecore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NISSitecore.Feature.Media.Constants;

namespace NISSitecore.Feature.Media.Controllers
{
    public class MediaController : SitecoreController
    {
        private readonly ICarouselMediator _carouselMeidator;

        public MediaController(ICarouselMediator carouselMediator)
        {
            _carouselMeidator = carouselMediator;
        }
        // GET: Media
        public ActionResult Carousel()
        {
            var mediatorResponse = _carouselMeidator.RequestCarouselViewModel();

            switch(mediatorResponse.Code)
            {
                case MediatorCodes.MediaResponse.DataSourceError:
                case MediatorCodes.MediaResponse.ViewModelError:
                    return View("~/views/Hero/Error.cshtml");
                case MediatorCodes.MediaResponse.Ok:
                    return View(mediatorResponse.ViewModel);
                default:
                    throw new InvalidMediatorResponseCodeException(mediatorResponse.Code);
            }
        }
    }
}