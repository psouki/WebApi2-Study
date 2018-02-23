using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerDev.Models
{
    public class CatalogVm
    {
        public string BeerId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
    }
}