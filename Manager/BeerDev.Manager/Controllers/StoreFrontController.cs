using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BeerDev.Entities;
using BeerDev.Models;
using BeerDev.Repository.Interfaces;
using BeerDev.Repository.Repositories;

namespace BeerDev.Manager.Controllers
{
    [RoutePrefix("api/storeFront")]
    public class StoreFrontController : ApiController
    {
        private readonly IStoreFrontRepository _repository;
        public StoreFrontController()
        {
            _repository = new StoreFrontRepository();
        }

        public IHttpActionResult Get()
        {
            IEnumerable<StoreFront> catalog = _repository.GetAll();

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
    }
}
