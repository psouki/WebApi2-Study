﻿using System.Web.Http;
using System.Web.Http.Cors;
using BeerDev.Api.Models;

namespace BeerDev.Api.Controllers
{
    [EnableCors("*", "*", "GET,POST")]
    public class GiftController : ApiController
    {
        public IHttpActionResult Post(GiftVm gift)
        {
            GiftPromoVm giftPromo = new GiftPromoVm();

            if (gift.InvoiceAverage != "high" && gift.CustomerCategory != "gold") return Ok(giftPromo);
            switch (gift.BuyingStyle)
            {
                case "same":
                    giftPromo.Name = "discount";
                    giftPromo.Description = "10% discount";
                    giftPromo.Helps = "To pay less.";
                    break;
                case "always new":
                    giftPromo.Name = "t-shirt";
                    giftPromo.Description = "Love for beer";
                    giftPromo.Helps = "To express your love for the beer.";
                    break;
                default:
                    giftPromo.Name = "cup";
                    giftPromo.Description = "cup for devBeer";
                    giftPromo.Helps = "To drink with more pleasure.";
                    break;
            }

            return Ok(giftPromo);
        }

    }
}
