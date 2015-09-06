using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Tatabouf.Domain;

namespace Tatabouf.DAL.Configurations
{
    public class PlaceConfiguration : EntityTypeConfiguration<Place>
    {
        public PlaceConfiguration()
        {
            ToTable("place");

            Property(t => t.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Label).HasColumnName("name");
            Property(t => t.DisplayOrder).HasColumnName("display_order");

            HasKey(t => t.Id);
        }
    }
}