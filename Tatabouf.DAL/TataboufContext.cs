using System.Data.Entity;
using Tatabouf.DAL.Configurations;
using Tatabouf.Domain;

namespace Tatabouf.DAL
{
    public class TataboufContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        //public DbSet<Choice> Choices { get; set; }
                
        static TataboufContext()
        {
            Database.SetInitializer<TataboufContext>(null);
        }

        public TataboufContext()
            : base("TataboufContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var configuration = modelBuilder.Configurations;            
            configuration.Add(new UserConfiguration());
            configuration.Add(new PlaceConfiguration());
            configuration.Add(new ChoicesConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}