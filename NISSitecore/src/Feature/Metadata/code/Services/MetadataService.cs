using NISSitecore.Feature.Metadata.Models;
using NISSitecore.Foundation.Content.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISSitecore.Feature.Metadata.Services
{
    public class MetadataService : IMetadataService
    {
        private readonly IContextRepository _contextRepository;
        private readonly IRenderingRepository _renderingRepository;

        public MetadataService(IContextRepository contextRepository, IRenderingRepository renderingRepository)
        {
            _contextRepository = contextRepository;
            _renderingRepository = renderingRepository;
        }

        public IPageMetadata GetPageMetadataItems()
        {
            var dataSource = _contextRepository.GetCurrentItem<IPageMetadata>();

            if (dataSource == null)
            {
                //Log the error
                //Sitecore.Diagnostics.Log.Warn("Datasource is null");
            }

            return dataSource;
        }

        public ISiteMetadata GetSiteMetadataItems()
        {
            var dataSource = _contextRepository.GetCurrentItem<ISiteMetadata>();

            if (dataSource == null)
            {
                //Log the error
                //Sitecore.Diagnostics.Log.Warn("Datasource is null");
            }

            return dataSource;
        }

        public bool IsExperienceEditor => _contextRepository.IsExperienceEditor;
    }
}