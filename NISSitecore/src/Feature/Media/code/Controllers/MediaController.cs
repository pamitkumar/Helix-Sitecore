using Glass.Mapper.Sc.Web.Mvc;
using NISSitecore.Feature.Media.Mediators;
using NISSitecore.Feature.Media.Models;
using NISSitecore.Foundation.Content.Repositories;
using NISSitecore.Foundation.Core.Exceptions;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;
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
        private readonly IContextRepository _contextRepository;
        private readonly IRenderingRepository _renderingRepository;

        public MediaController(ICarouselMediator carouselMediator, IContextRepository contextRepository, IRenderingRepository renderingRepository)
        {
            _carouselMeidator = carouselMediator;
            _contextRepository = contextRepository;
            _renderingRepository = renderingRepository;
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

        public ActionResult PageBoxContent()
        {
            #region Without Glass mapper


            // var sourceItem = RenderingContext.Current.Rendering.Item;

            #region Mapping Fields

            //var pageModel = new PageModel();
            //pageModel.Title = sourceItem["Title"];
            //pageModel.Content = sourceItem["Content"];
            //ImageField imgField = sourceItem.Fields["Image"];
            //if (imgField != null)
            //{
            //    //pageModel.Image = imgField.MediaItem.Uri;
            //    //pageModel.Image = new Glass.Mapper.Sc.Fields.Image()
            //    //{
            //    //    Src = Sitecore.Resources.Media.MediaManager.GetMediaUrl(imgField.MediaItem)
            //    //};
            //}

            //return View("~/Views/Media/PageBoxContent.cshtml", pageModel);

            #endregion

            #endregion


            #region With Glass mapper

            //IMvcContext mvcContext = new MvcContext();

            //var source = mvcContext.GetDataSourceItem<IPageModel>();            

            //return View("~/Views/Media/PageBoxContent.cshtml", source);


            #endregion

            #region With Content repository

            var dataSource = _renderingRepository.GetDataSourceItem<IPageModel>();

            return View("~/Views/Media/PageBoxContent.cshtml", dataSource);
            #endregion

        }
    }
}