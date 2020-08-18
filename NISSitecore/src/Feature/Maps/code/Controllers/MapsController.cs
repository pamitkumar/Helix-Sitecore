using System;
using System.Web.Mvc;
using Sitecore.Data;
using NISSitecore.Feature.Maps.Repositories;
using Sitecore;

namespace NISSitecore.Feature.Maps.Controllers
{
    public class MapsController : Controller
    {
        private readonly IMapPointRepository mapPointRepository;

        public MapsController(IMapPointRepository mapPointRepository)
        {
            this.mapPointRepository = mapPointRepository;
        }

        [HttpPost]        
        public JsonResult GetMapPoints(Guid itemId)
        {
            var item = Context.Database.GetItem(new ID(itemId));
            var points = this.mapPointRepository.GetAll(item);

            return this.Json(points);
        }
    }
}