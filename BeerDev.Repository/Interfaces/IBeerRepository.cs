using BeerDev.Entities;

namespace BeerDev.Repository.Interfaces
{
    //I don't need to created o that common operation for every entity
    // only the specialized methods will be here.
    // Open/close principle from SOLID
    public interface IBeerRepository : IRepositoryBase<Beer>
    {
    }
}
