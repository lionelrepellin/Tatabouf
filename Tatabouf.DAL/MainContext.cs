using System.Data.Entity;
using Tatabouf.DAL.Configurations;
using Tatabouf.Domain;

namespace Tatabouf.DAL
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
                
        static MainContext()
        {
            Database.SetInitializer<MainContext>(null);
        }

        public MainContext()
            : base("TataboufContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var configuration = modelBuilder.Configurations;            
            configuration.Add(new UserConfiguration());
            configuration.Add(new PlaceConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}