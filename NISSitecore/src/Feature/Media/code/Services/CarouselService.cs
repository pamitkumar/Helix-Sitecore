using NISSitecore.Feature.Media.Models;
using NISSitecore.Foundation.Content.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Media.Services
{
    public class CarouselService : ICarouselService
    {
        private readonly IContextRepository _contextRepository;        
        private readonly IRenderingRepository _renderingRepository;

        public CarouselService(IContextRepository contextRepository,IRenderingRepository renderingRepository)
        {
            _contextRepository = contextRepository;
            _renderingRepository = renderingRepository;
        }
        public bool IsExperienceEditor => _contextRepository.IsExperienceEditor;

        /// <summary>
        /// Get an item using rendering repository
        /// </summary>
        /// <returns></returns>
        public ICarousel GetCarouselItems()
        {
            var dataSource = _renderingRepository.GetDataSourceItem<ICarousel>();

            if (dataSource == null)
            {
                //Log the error
                //Sitecore.Diagnostics.Log.Warn("Datasource is null");
            }

                return dataSource;
        }

        
    }
}