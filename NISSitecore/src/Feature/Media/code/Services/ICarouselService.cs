using NISSitecore.Feature.Media.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NISSitecore.Feature.Media.Services
{
    public interface ICarouselService
    {
        ICarousel GetCarouselItems();
        bool IsExperienceEditor { get; }
    }
}
