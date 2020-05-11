using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Media.Models
{
    public interface ICarousel:IMediaGlassBase
    {
        [SitecoreField("Intro")]
        string Intro { get; set; }
        [SitecoreField("Title")]
        string Title { get; set; }
        [SitecoreField("Caption")]
        string Caption { get; set; }

        [SitecoreField("Additional Text")]
        string AdditionalText { get; set; }
        IEnumerable<ICarouselSlide> Images { get; set; }

    }
}
