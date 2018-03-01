namespace BeerDev.Entities
{
    // This pseudo representation of the real world is in the center of the application
    // it doesn't change for no one, every part change for them
    // It is use a project containing the entity just to show that
    // this is a unavoidable complexity(cost) to make it maintainable in its use years
    public class StoreFront
    {
        public int StoreFrontId { get; set; }
        public Beer Beer { get; set; }
        public string ShowPlace { get; set; }
    }
}
