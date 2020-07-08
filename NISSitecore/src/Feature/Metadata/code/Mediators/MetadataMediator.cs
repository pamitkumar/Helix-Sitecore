using Glass.Mapper.Sc;
using NISSitecore.Feature.Metadata.Models;
using NISSitecore.Feature.Metadata.Services;
using NISSitecore.Feature.Metadata.ViewModels;
using NISSitecore.Foundation.Core.Models;
using NISSitecore.Foundation.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static NISSitecore.Feature.Metadata.Constants;

namespace NISSitecore.Feature.Metadata.Mediators
{
    public class MetadataMediator : IMetadaMediator
    {
        private readonly IMediatorService _mediatorService;
        private readonly IMetadataService _metadataService;
        private readonly IGlassHtml _glassHtml;

        public MetadataMediator(IMediatorService mediatorService, IMetadataService metadataService, IGlassHtml glassHtml)
        {
            _mediatorService = mediatorService;
            _metadataService = metadataService;
            _glassHtml = glassHtml;
        }
        public MediatorResponse<MetadataViewModel> RequestMetadataViewModel()
        {
            var metaDataItemsource = _metadataService.GetPageMetadataItems();
            var siteMetadataItemSource = _metadataService.GetSiteMetadataItems();

            if (metaDataItemsource == null)
                return _mediatorService.GetMediatorResponse<MetadataViewModel>(MediatorCodes.MediaResponse.DataSourceError);

            var viewModel = CreateMetadataViewModel(metaDataItemsource, siteMetadataItemSource, _metadataService.IsExperienceEditor);

            if (viewModel == null)
                return _mediatorService.GetMediatorResponse<MetadataViewModel>(MediatorCodes.MediaResponse.ViewModelError);

            return _mediatorService.GetMediatorResponse<MetadataViewModel>(MediatorCodes.MediaResponse.Ok, viewModel);
        }

        public MetadataViewModel CreateMetadataViewModel(IPageMetadata metadataItemDataSource, ISiteMetadata siteMetadata, bool isExperienceEditor)
        {

            var model = new MetadataViewModel();

            this.SetKeywords(model, metadataItemDataSource);
            this.SetPageTitle(model, metadataItemDataSource);
            this.SetDescription(model, metadataItemDataSource);
            this.SetIndexingFlags(model, metadataItemDataSource);
            this.SetCustomMetadata(model, metadataItemDataSource);
            this.CombineTitle(model, siteMetadata);

            return model;
        }


        #region Helper Methods

        private void SetCustomMetadata(MetadataViewModel model, IPageMetadata item)
        {
            ICollection<KeyValuePair<string, string>> customMetadata = new List<KeyValuePair<string, string>>();

            var values = item.CustomMetaData;
            if (values == null)
                return;
            foreach (var key in values.AllKeys)
            {
                var value = HttpUtility.UrlDecode(values[key]);
                customMetadata.Add(new KeyValuePair<string, string>(key, value));
            }

            model.CustomMetadata = customMetadata;
        }

        private void SetDescription(MetadataViewModel model, IPageMetadata item)
        {
            var description = item.MetaDescription;
            if (string.IsNullOrWhiteSpace(description))
                return;
            model.Description = description;
        }

        private void SetPageTitle(MetadataViewModel model, IPageMetadata metadata)
        {
            var title = metadata.BrowserTitle;
            if (string.IsNullOrWhiteSpace(title))
                return;
            model.PageTitle = title;
        }

        private void SetIndexingFlags(MetadataViewModel model, IPageMetadata metadata)
        {
            ICollection<string> robotsMetadata = new List<string>();

            if (!(metadata.CanIndex == true))
            {
                robotsMetadata.Add("NOINDEX");
            }
            if (!(metadata.SeoFollowLinks == true))
            {
                robotsMetadata.Add("NOFOLLOW");
            }
            model.Robots = robotsMetadata;
        }

        private void SetKeywords(MetadataViewModel model, IPageMetadata metadata)
        {

            ICollection<string> _keywordsList = new List<string>();

            var keywordMultilist = metadata.MetaKeywords.ToList();
            if (keywordMultilist != null)
            {
                foreach (var key in keywordMultilist)
                {
                    _keywordsList.Add(key.Name);
                }
                model.KeywordsList = _keywordsList;
            }

        }

        private void CombineTitle(MetadataViewModel model, ISiteMetadata siteMetadata)
        {
            model.Title = model.PageTitle + siteMetadata.SiteBrowserTitle;
        }

        #endregion

    }
}