using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;
using BeerDev.Entities;
using BeerDev.Models;
using BeerDev.Repository.Interfaces;
using BeerDev.Repository.Repositories;
using BeerDev.Util;

namespace BeerDev.ControllersApi
{
    public class HomeController : ApiController
    {
        private readonly IStoreFrontRepository _repository;

        public HomeController()
        {
            _repository = new StoreFrontRepository();
        }

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
    }
}
