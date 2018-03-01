using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BeerDev.Entities;
using BeerDev.Manager.Models;
using BeerDev.Repository.Interfaces;
using BeerDev.Repository.Repositories;

namespace BeerDev.Manager.Controllers
{
    // this overrides any similar route in the WepApi.config
    // This tells that all routes in this controller starts with api/beers
    [RoutePrefix("api/beers")]
    public class BeersController : ApiController
    {
        private readonly IBeerRepository _beerRepository;

        public BeersController()
        {
            _beerRepository = new BeerRepository();
        }

        // this is the default GET route (api/beers)
        // this can be used as a parameterless action or we can pass by query string 
        // these two parameter
        public IHttpActionResult Get(int count = 0, string sort = "id")
        {
            // here we have the clear distinction of entity and view dtos.
            IEnumerable<Beer> beers = _beerRepository.GetAll();

            // this two can grow independently. 
            // using the separation of concerns, this act like the adapter pattern.
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

            IEnumerable<BeerVm> result = count != 0 ? catalog.Take(count) : catalog;
            result = SortBeers(result, sort);
            return Ok(result);
        }

        // Route api/beers/{id}
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

        // Route api/beers/beer/{id}
        // We use the route prefix above the facilitate extensions of routes in the methods
        [Route("beer/{id}")]
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

        //This verb is use as general purpose, but it shouldn't 
        // as soon as the resource is recoverable, that means, we know its id
        // we should use PUT because it is idempotent 
        // POST: api/Beers
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

        // Here we already now its is so it a better verb to use.
        // In case that we now in advance which id the resource will receive
        // it could be use the PUT as well, just because idempotent are safer.
        // PUT: api/Beers/{id}
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

        // DELETE: api/Beers/{id}
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

        // This is used for partial update,
        // in case that we desire to send only few properties of a large entity 
        // it is possible with this verb, The PUT needs the whole entity to be valid request
        // PATCH: api/Beers/{id}
        public IHttpActionResult Patch(int id, Beer beerVm)
        {
            if (beerVm == null)
            {
                return BadRequest();
            }
            try
            {
                Beer beer = _beerRepository.GetById(id);
                if (beer == null) return NotFound();

                beer.Alchool = beerVm.Alchool == 0 ? beer.Alchool : beerVm.Alchool;
                beer.Code = beerVm.Code ?? beer.Code;
                beer.Description = beerVm.Description ?? beer.Description;
                beer.Kind = beerVm.Kind ?? beer.Kind;
                beer.Name = beerVm.Name ?? beer.Name;
                beer.Nationality = beerVm.Nationality ?? beer.Nationality;
                beer.Picture = beerVm.Picture ?? beer.Picture;
                beer.PictureThumbnail = beerVm.PictureThumbnail ?? beer.PictureThumbnail;
                beer.Price = beerVm.Price == 0 ? beer.Price : beerVm.Price;

                _beerRepository.Update(beer);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        #region Auxiliary methods
        // in a real world application it will became a generic method
        // but it is a unnecessary complexity(cost) for this study.
        private IEnumerable<BeerVm> SortBeers(IEnumerable<BeerVm> result, string sort)
        {
            switch (sort.ToLower())
            {
                case "id":
                    return result.OrderBy(b => b.BeerId);
                case "alcohol":
                    return result.OrderBy(b => b.Alchool);
                case "kind":
                    return result.OrderBy(b => b.Kind);
                case "name":
                    return result.OrderBy(b => b.Name);
                default:
                    return result.OrderBy(b => b.BeerId);
            }
        }
        #endregion
    }
}
