using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BeerDev.Entities;
using BeerDev.Manager.Models;
using BeerDev.Repository.Interfaces;
using BeerDev.Repository.Repositories;

namespace BeerDev.Manager.Controllers
{
    public class BeersController : ApiController
    {
        private readonly IBeerRepository _beerRepository;

        public BeersController()
        {
            _beerRepository = new BeerRepository();
        }

        public IHttpActionResult Get()
        {
            IEnumerable<Beer> beers = _beerRepository.GetAll();
            IEnumerable<BeerVm> catalog = beers.Select(c => new BeerVm
            {
                Alchool = c.Alchool,
                BeerId = c.BeerId,
                Code = c.Code,
                Description = c.Description,
                Kind = c.Kind,
                Name = c.Name,
                Nationality = c.Nationality,
                Picture = c.Picture,
                PictureThumbnail = c.PictureThumbnail,
                Price = c.Price
            }).ToList();

            return Ok(catalog);
        }

        //[Route("api/beers/{id}")]
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
                BeerId = beer.BeerId,
                Code = beer.Code,
                Description = beer.Description,
                Kind = beer.Kind,
                Name = beer.Name,
                Nationality = beer.Nationality,
                Picture = beer.Picture,
                PictureThumbnail = beer.PictureThumbnail,
                Price = beer.Price
            };

            return Ok(result);
        }

        [Route("api/beers/beer/{id}")]
        public IHttpActionResult GetById(int id)
        {
            Beer beer = _beerRepository.GetById(id);

            if (beer == null)
            {
                return NotFound();
            }

            BeerVm result = new BeerVm
            {
                Alchool = beer.Alchool,
                BeerId = beer.BeerId,
                Code = beer.Code,
                Description = beer.Description,
                Kind = beer.Kind,
                Name = beer.Name,
                Nationality = beer.Nationality,
                Picture = beer.Picture,
                PictureThumbnail = beer.PictureThumbnail,
                Price = beer.Price
            };

            return Ok(result);
        }

        //// POST: api/Beers
        //[HttpPost]
        public IHttpActionResult Post([FromBody]BeerVm beerVm)
        {
            if (beerVm == null)
            {
                return BadRequest();
            }

            try
            {
                Beer beer = new Beer
                {
                    Alchool = beerVm.Alchool,
                    BeerId = beerVm.BeerId,
                    Code = beerVm.Code,
                    Description = beerVm.Description,
                    Kind = beerVm.Kind,
                    Name = beerVm.Name,
                    Nationality = beerVm.Nationality,
                    Picture = beerVm.Picture,
                    PictureThumbnail = beerVm.PictureThumbnail,
                    Price = beerVm.Price
                };
                _beerRepository.Add(beer);

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, Beer beerVm)
        {
            if (beerVm == null)
            {
                return BadRequest();
            }
            try
            {
                Beer beer = _beerRepository.GetById(id);
                if (beer == null) return NotFound();

                beer.Alchool = beerVm.Alchool;
                beer.BeerId = beerVm.BeerId;
                beer.Code = beerVm.Code;
                beer.Description = beerVm.Description;
                beer.Kind = beerVm.Kind;
                beer.Name = beerVm.Name;
                beer.Nationality = beerVm.Nationality;
                beer.Picture = beerVm.Picture;
                beer.PictureThumbnail = beerVm.PictureThumbnail;
                beer.Price = beerVm.Price;

                _beerRepository.Update(beer);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(int id = 0)
        {
            if (id == 0) return BadRequest();

            try
            {
                Beer beer = _beerRepository.GetById(id);
                if (beer == null) return NotFound();

                _beerRepository.Delete(beer);

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }
    }
}
