using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BeerDev.Entities;
using BeerDev.Models;
using BeerDev.Repository.Interfaces;
using BeerDev.Repository.Repositories;
using BeerDev.Util;

namespace BeerDev.ControllersApi
{
    public class AllBeersController : ApiController
    {
        private readonly IBeerRepository _beerRepository;

        public AllBeersController()
        {
            _beerRepository = new BeerRepository();
        }

        public IHttpActionResult Get()
        {
            IEnumerable<Beer> beers = _beerRepository.GetAll();
            IEnumerable<CatalogVm> catalog = beers.Select(c => new CatalogVm
            {
                Picture = c.PictureThumbnail,
                Price = c.Price,
                BeerId = c.Code,
                Description = c.Description,
                Name = c.Name
            }).ToList();
           
            return Ok(catalog);
        }

        [Route("api/AllBeers/{id}")]
        public IHttpActionResult Get(string id)
        {
            Beer beer = _beerRepository.Get(b => b.Code == id);

            if (beer == null)
            {
                return NotFound();
            }

            BeerVm result = new BeerVm
            {
                Alchool = beer.Alchool,
                BeerId = beer.Code,
                Description = beer.Description,
                Kind = beer.Kind,
                Name = beer.Name,
                Nationality = beer.Nationality,
                Picture = beer.Picture,
                Price = beer.Price
            };

            return Ok(result);
        }

        [Route("api/AllBeers/Sales")]
        [HttpGet]
        public IHttpActionResult GetSales()
        {
            IEnumerable<string> months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            const int minimum = 97;
            Random rnd = new Random();

            ICollection<SalesVm> salesData = months.Select(month => new SalesVm
            {
                Sales = rnd.Next(3, 13) * rnd.Next(3, 13) + minimum,
                Month = month
            }).ToList();
   
            if (salesData.Any())
            {
               return Ok(salesData);
            }
            return NotFound();
        }
    }
}
