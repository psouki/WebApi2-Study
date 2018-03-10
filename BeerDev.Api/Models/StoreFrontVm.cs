using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerDev.Models
{
    public class StoreFrontVm
    {
        public string BeerId { get; set; }
        public string Front { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
    }
}