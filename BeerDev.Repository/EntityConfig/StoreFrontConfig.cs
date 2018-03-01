using System.Data.Entity.ModelConfiguration;
using BeerDev.Entities;

namespace BeerDev.Repository.EntityConfig
{
    // here we define that every Storefront must have a beer
    // and the entities doesn't need the has a database
    public class StoreFrontConfig : EntityTypeConfiguration<StoreFront>
    {
        public StoreFrontConfig()
        {
            HasRequired(s => s.Beer);
        }
    }
}
