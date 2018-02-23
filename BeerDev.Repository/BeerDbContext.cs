using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BeerDev.Entities;
using BeerDev.Repository.EntityConfig;

namespace BeerDev.Repository
{
    public class BeerDbContext : DbContext
    {
        public BeerDbContext() : base("BeerConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<StoreFront> StoreFronts { get; set; }
        public DbSet<PictureGallery> PictureGalleries { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p=> p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p=>p.HasMaxLength(100));

            modelBuilder.Properties<string>()
                .Configure(p=>p.IsRequired());

            modelBuilder.Properties<decimal>()
                .Configure(p=>p.HasPrecision(10,2));

            modelBuilder.Configurations.Add(new BeerConfig());
            modelBuilder.Configurations.Add(new StoreFrontConfig());
        }
    }
}
