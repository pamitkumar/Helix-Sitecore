using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace NISSitecore.Feature.Media.Models
{
    public interface ICarouselSlide:IMediaGlassBase
    {
        [SitecoreField("Title")]
        string Title { get; set; }
        [SitecoreField("Caption")]
        string Caption { get; set; }
        [SitecoreField("Image")]
        Image Image { get; set; }
    }
}
