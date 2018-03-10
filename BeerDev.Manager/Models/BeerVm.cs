using System.ComponentModel;
using System.Web.Mvc;

namespace BeerDev.Manager.Models
{
    public class BeerVm
    {
        [HiddenInput(DisplayValue = false)]
        public int BeerId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Kind { get; set; }
        public decimal Alchool { get; set; }
        public string Code { get; set; }
        public string Picture { get; set; }
        [DisplayName("Picture Thumbnail")]
        public string PictureThumbnail { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}