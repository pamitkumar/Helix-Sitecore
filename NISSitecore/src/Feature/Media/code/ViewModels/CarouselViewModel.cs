using NISSitecore.Feature.Media.Models;
using System.Collections.Generic;
using System.Web;

namespace NISSitecore.Feature.Media.ViewModels
{
    public class CarouselViewModel
    {

        public HtmlString Intro { get; set; }
        public HtmlString CarouselTitle { get; set; }
        public HtmlString CarouselCaption { get; set; }
        public HtmlString AdditionalText { get; set; }
        public IEnumerable<ICarouselSlide> Slides { get; set; }
        public bool IsExperienceEditor { get; set; }
    }
}