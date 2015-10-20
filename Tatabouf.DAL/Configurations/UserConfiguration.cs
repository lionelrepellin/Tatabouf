using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tatabouf.Domain;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tatabouf.DAL.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("user");

            Property(t => t.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).HasColumnName("name");
            Property(t => t.AvailableSeats).HasColumnName("available_seats");
            Property(t => t.InscriptionDate).HasColumnName("inscription_date");
            Property(t => t.DepartureTime).HasColumnName("departure_time");
            Property(t => t.IpAddress).HasColumnName("ip_address");

            HasKey(t => t.Id);

            HasMany(t => t.Choices).WithRequired(t => t.User);
        }
    }
}