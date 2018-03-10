using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using BeerDev.Api.Helpers;
using BeerDev.Entities;
using BeerDev.Models;
using BeerDev.Repository.Interfaces;
using BeerDev.Repository.Repositories;

namespace BeerDev.Api.Controllers
{
    [RoutePrefix("api/storeFront")]
    [EnableCors("*", "*", "GET")]
    public class StoreFrontController : ApiController
    {
        private readonly IStoreFrontRepository _repository;

        public StoreFrontController()
        {
            _repository = new StoreFrontRepository();
        }

        // use a custom route constraint is the best practice to version route,
        // because is the same resource, build other route is another resource
        [VersionRoute("", 1)]
        public IHttpActionResult Get()
        {
            IEnumerable<StoreFront> front = _repository.GetAll(s => s.Beer);

            if (front == null) return NotFound();

            IEnumerable<StoreFrontVm> result = front.Select(c => new StoreFrontVm
            {
                Picture = c.Beer?.PictureThumbnail,
                Price = c.Beer?.Price ?? 0,
                BeerId = c.Beer?.Code,
                Name = c.Beer?.Name,
                Front = c.ShowPlace
            }).ToList();

            return Ok(result);
        }
        // with we can define each route to chose, and if old caller don't specify the version
        // the default route will be chosen. this comply with the Open/Close principle of the SOLID
        [VersionRoute("", 2)]
        public IHttpActionResult GetV2()
        {
            IEnumerable<StoreFront> front = _repository.GetAll(s => s.Beer);

            if (front == null) return NotFound();

            IEnumerable<StoreFrontVm> result = front.Select(c => new StoreFrontVm
            {
                Picture = c.Beer?.PictureThumbnail,
                Price = c.Beer?.Price - 2 ?? 0,
                BeerId = c.Beer?.Code,
                Name = c.Beer?.Name,
                Front = c.ShowPlace
            }).ToList();

            return Ok(result);
        }
    }
}
