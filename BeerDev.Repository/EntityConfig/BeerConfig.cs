using System.Data.Entity.ModelConfiguration;
using BeerDev.Entities;

namespace BeerDev.Repository.EntityConfig
{
    // with this class we can override the setting of every string will be varchar(100)
    public class BeerConfig :EntityTypeConfiguration<Beer>
    {
        public BeerConfig()
        {
            Property(b=>b.Description).HasColumnType("varchar(max)");
        }
    }
}
