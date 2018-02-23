using System.Collections;
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
    public class RankingController : ApiController
    {
        private readonly IBeerRepository _repository;

        public RankingController()
        {
            _repository = new BeerRepository();
        }
        public IHttpActionResult Get()
        {
            IEnumerable<Beer> beers = _repository.GetAll();
            IEnumerable<RankingVm> rankingBeers = beers.Select(r => new RankingVm
            {
                BeerId = r.Code,
                Name =  r.Name
            }).ToList();

            string result = JsonHelper<RankingVm>.Serialize(rankingBeers);
            
            if (string.IsNullOrEmpty(result))
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
