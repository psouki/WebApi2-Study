namespace BeerDev.Entities
{
    public class Beer
    {
        public int BeerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureThumbnail { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Nationality { get; set; }
        public string Kind { get; set; }
        public decimal Alchool { get; set; }
    }
}
