using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;
using BeerDev.Models;
using Newtonsoft.Json;

namespace BeerDev.ControllersApi
{
    public class AllBeersController : ApiController
    {
        public IHttpActionResult Get()
        {
            string result;
            using (StreamReader sr = new StreamReader(HostingEnvironment.MapPath("~/mockData/beerFront.json")))
            {
                result = sr.ReadToEnd();
            }

            if (string.IsNullOrEmpty(result))
            {
                return NotFound();
            }

            return Ok(result);
        }

        public IHttpActionResult Get(string id)
        {
            IEnumerable<Beer> beers = new List<Beer>();
            using (StreamReader sr = new StreamReader(HostingEnvironment.MapPath("~/mockData/beerCatalog.json")))
            {
                beers = JsonConvert.DeserializeObject<IEnumerable<Beer>>(sr.ReadToEnd());
            }

            Beer result = beers.FirstOrDefault(b => b.beerId.Equals(id));

            if (result == null)
            {
               return NotFound();
            }
            return Ok(result);
        }

        [Route("api/AllBeers/Sales")]
        [HttpGet]
        public IHttpActionResult GetSales()
        {
            IEnumerable<string> months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            const int minimum = 97;
            Random rnd = new Random();

            ICollection<Sales> salesData = months.Select(month => new Sales
            {
                sales = rnd.Next(3, 13) * rnd.Next(3, 13) + minimum,
                month = month
            }).ToList();

            if (salesData.Any())
            {
               return Ok(salesData);
            }
            return NotFound();
        }
    }
}
