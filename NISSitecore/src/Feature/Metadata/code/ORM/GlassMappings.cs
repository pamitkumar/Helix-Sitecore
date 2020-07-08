using Glass.Mapper;
using Glass.Mapper.Sc.Maps;
using NISSitecore.Feature.Metadata.Models;

namespace NISSitecore.Feature.Metadata.ORM
{
    public class GlassMappings : SitecoreGlassMap<IHomePage>
    {
        public override void Configure()
        {
            Map(config =>

            {
                config.TemplateId(Constants.HomePage.TemplateId);
                config.AutoMap();
            });
        }
    }

    public class PageMetaMap : SitecoreGlassMap<IPageMetadata>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.TemplateId(Constants.PageMetadata.TemplateId);
                config.AutoMap();
            });
        }
    }

    public class KeywordMap:SitecoreGlassMap<IKeyword>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.TemplateId(Constants.Keyword.TemplateId);
                config.AutoMap();
            });
        }
    }

    public class SiteMetadataMap : SitecoreGlassMap<ISiteMetadata>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.TemplateId(Constants.SiteMetadata.TemplateId);
                config.AutoMap();
            });
        }
    }

    public class MetadataKeywordMap : SitecoreGlassMap<IMetaKeyword>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.TemplateId(Constants.MetaKeyword.TemplateId);
                config.AutoMap();
            });
        }
    }

    public class GlassBaseMappings : SitecoreGlassMap<IMetadataGlassBase>
    {
        public override void Configure()
        {
            Map(config =>
            {
                config.AutoMap();
            });
        }
    }

}