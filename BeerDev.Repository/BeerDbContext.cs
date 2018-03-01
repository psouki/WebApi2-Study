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

        // Is good practice to override some of the out of the box configuration
        // What is done by the framework by default is very good, but no means 
        // is complete to use in the production environment. It is just the beginning. 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //instead of pollute the entities with sql dependency annotations 
            
            // every id is configured here because, using the philosophy convention over configuration
            // is defined that every key will be the entity name + Id
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            // every string will be converted in varchar in the database
            modelBuilder.Properties<string>()
                .Configure(p=> p.HasColumnType("varchar"));

            // every string will have a default value of 100 characters 
            modelBuilder.Properties<string>()
                .Configure(p=>p.HasMaxLength(100));

            // every string will be required
            modelBuilder.Properties<string>()
                .Configure(p=>p.IsRequired());

            // every decimal will be have the precision of 10, 2
            modelBuilder.Properties<decimal>()
                .Configure(p=>p.HasPrecision(10,2));

            // every time it is need to override this conventions, 
            // and set relationships  we can use this classes
            modelBuilder.Configurations.Add(new BeerConfig());
            modelBuilder.Configurations.Add(new StoreFrontConfig());
        }
    }
}
