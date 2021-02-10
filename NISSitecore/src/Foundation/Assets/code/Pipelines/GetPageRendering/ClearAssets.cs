﻿
namespace NISSitecore.Foundation.Assets.Pipelines.GetPageRendering
{
    using NISSitecore.Foundation.Assets.Repositories;
    using Sitecore.Mvc.Pipelines.Response.GetPageRendering;

    public class ClearAssets : GetPageRenderingProcessor
    {
        public override void Process(GetPageRenderingArgs args)
        {
            AssetRepository.Current.Clear();
        }
    }
}