using Glass.Mapper.Sc.Maps;
using NISSitecore.Feature.Media.Models;
using System;

namespace NISSitecore.Feature.Media.ORM
{
    public class GlassMappings : SitecoreGlassMap<ICarousel>
    {
        public override void Configure()
        {
            Map(config =>
            {                
                config.TemplateId(Constants.Carousel.TemplateId);
                //config.Field(f => f.Intro).FieldName("Intro");
                //config.Field(f => f.Title).FieldName("Title");
                //config.Field(f => f.Caption).FieldName("Caption");
                config.Field(f => f.AdditionalText).FieldName("Additional Text");
                //config.Field(f => f.Images).FieldName("Images");                
                config.AutoMap();
            });
        }

        //public class CarouselSlideMap : SitecoreGlassMap<ICarouselSlide>
        //{
        //    public override void Configure()
        //    {
        //        Map(x =>
        //        {
        //            x.TemplateId(Constants.CarouselSlide.TemplateId);
        //            x.AutoMap();
        //        });
        //    }
        //}

        public class GlassBaseMappings : SitecoreGlassMap<IMediaGlassBase>
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
}