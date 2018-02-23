using System.Data.Entity.ModelConfiguration;
using BeerDev.Entities;

namespace BeerDev.Repository.EntityConfig
{
    public class BeerConfig :EntityTypeConfiguration<Beer>
    {
        public BeerConfig()
        {
            Property(b=>b.Description).HasColumnType("varchar(max)");
        }
    }
}
