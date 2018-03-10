using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BeerDev.Entities;
using BeerDev.Manager.Const;
using BeerDev.Manager.Helper;
using BeerDev.Manager.Models;
using Newtonsoft.Json;

namespace BeerDev.Manager.Controllers
{
    public class BeersController : Controller
    {
        // GET: Beers
        public async Task<ActionResult> Index()
        {
            var client = HttpClientHelper.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/beers");

            if (!response.IsSuccessStatusCode) return View(new List<BeerVm>());

            string result = await response.Content.ReadAsStringAsync();
            IEnumerable<Beer> beers = string.IsNullOrEmpty(result)
                ? new List<Beer>()
                : JsonConvert.DeserializeObject<IEnumerable<Beer>>(result);

            IEnumerable<BeerVm> model = beers.Select(r => new BeerVm
            {
                Name = r.Name,
                Price = r.Price,
                BeerId = r.BeerId,
                Code = r.Code,
                Picture = r.Picture,
                PictureThumbnail = r.PictureThumbnail,
                Description = r.Description,
                Alchool = r.Alchool,
                Nationality = r.Nationality,
                Kind = r.Kind
            });

            return View(model);
        }


        // GET: Beers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beers/Create
        [HttpPost]
        public async Task<ActionResult> Create(BeerVm beer)
        {
            try
            {
                string data = JsonHelper<BeerVm>.Serialize(beer);
                var client = HttpClientHelper.GetClient();
                var response = await client.PostAsync("api/beers", new StringContent(data, Encoding.Unicode, Format.Json));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return Content("An error occured");
            }
            catch
            {
                return Content("An error occured");
            }
        }

        // GET: Beers/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var client = HttpClientHelper.GetClient();
            HttpResponseMessage response = await client.GetAsync($"api/beers/beer/{id}");

            if (!response.IsSuccessStatusCode) return Content("An error occured");

            string result = await response.Content.ReadAsStringAsync();
            Beer beer = string.IsNullOrEmpty(result)
                ? new Beer()
                : JsonConvert.DeserializeObject<Beer>(result);

            BeerVm model = new BeerVm
            {
                Name = beer.Name,
                Price = beer.Price,
                BeerId = beer.BeerId,
                Code = beer.Code,
                Picture = beer.Picture,
                PictureThumbnail = beer.PictureThumbnail,
                Description = beer.Description,
                Alchool = beer.Alchool,
                Nationality = beer.Nationality,
                Kind = beer.Kind
            };
            return View(model);
        }

        // POST: Beers/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, BeerVm beer)
        {
            try
            {
                string data = JsonHelper<BeerVm>.Serialize(beer);
                var client = HttpClientHelper.GetClient();
                var response = await client.PutAsync($"api/beers/{id}", new StringContent(data, Encoding.Unicode, Format.Json));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return Content("An error occured");
            }
            catch
            {
                return Content("An error occured");
            }
        }

        // GET: Beers/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var client = HttpClientHelper.GetClient();
            HttpResponseMessage response = await client.GetAsync($"api/beers/beer/{id}");

            if (!response.IsSuccessStatusCode) return Content("An error occured");

            string result = await response.Content.ReadAsStringAsync();
            Beer beer = string.IsNullOrEmpty(result)
                ? new Beer()
                : JsonConvert.DeserializeObject<Beer>(result);

            BeerVm model = new BeerVm
            {
                Name = beer.Name,
                BeerId = beer.BeerId,
                Code = beer.Code,
            };
            return View(model);
        }

        // POST: Beers/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                var client = HttpClientHelper.GetClient();
                var response = await client.DeleteAsync($"api/beers/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return Content("An error occured");
            }
            catch
            {
                return Content("An error occured");
            }
        }
    }
}
