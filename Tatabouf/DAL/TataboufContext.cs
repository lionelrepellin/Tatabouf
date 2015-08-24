using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tatabouf.Domain;

namespace Tatabouf.DAL
{
    public class TataboufContext : DbContext
    {
        public DbSet<Crew> Dates { get; set; }
        
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
            configuration.Add(new CrewConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}