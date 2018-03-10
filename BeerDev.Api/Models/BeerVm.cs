namespace BeerDev.Api.Models
{
    //This is not a simple duplication of the entity
    // this is what the view needs, it is possible to change 
    // this DTO anytime, it will remain anemic just a DTO.
    // It gives us flexibility
    public class BeerVm
    {
        public int BeerId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Kind { get; set; }
        public decimal Alchool { get; set; }
        public string Picture { get; set; }
        public string PictureThumbnail { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}