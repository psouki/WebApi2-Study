using System.Data.Entity.ModelConfiguration;
using BeerDev.Entities;

namespace BeerDev.Repository.EntityConfig
{
    public class StoreFrontConfig : EntityTypeConfiguration<StoreFront>
    {
        public StoreFrontConfig()
        {
            HasRequired(s => s.Beer);
        }
    }
}
