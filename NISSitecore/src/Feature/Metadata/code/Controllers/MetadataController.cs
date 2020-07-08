using NISSitecore.Feature.Media.Models;
using NISSitecore.Feature.Metadata.Mediators;
using NISSitecore.Foundation.Core.Exceptions;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NISSitecore.Feature.Metadata.Constants;

namespace NISSitecore.Feature.Metadata.Controllers
{
    public class MetadataController : SitecoreController
    {
        private readonly IMetadaMediator _metadataMeidator;

        public MetadataController(IMetadaMediator metadataMediator)
        {
            _metadataMeidator = metadataMediator;
        }

        public ActionResult PageMetaData()
        {
            var mediatorResponse = _metadataMeidator.RequestMetadataViewModel();

            switch (mediatorResponse.Code)
            {
                case MediatorCodes.MediaResponse.DataSourceError:
                case MediatorCodes.MediaResponse.ViewModelError:
                    return View("~/views/Metadata/Error.cshtml");
                case MediatorCodes.MediaResponse.Ok:
                    return View(mediatorResponse.ViewModel);
                default:
                    throw new InvalidMediatorResponseCodeException(mediatorResponse.Code);
            }
        }       


    }
}