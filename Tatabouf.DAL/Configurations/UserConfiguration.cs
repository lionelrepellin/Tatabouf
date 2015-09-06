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
            Property(t => t.IGotMyLunch).HasColumnName("i_got_my_lunch");
            Property(t => t.AvailableSeats).HasColumnName("available_seats");
            Property(t => t.InscriptionDate).HasColumnName("inscription_date");
            Property(t => t.IpAddress).HasColumnName("ip_address");

            HasKey(t => t.Id);

            HasMany(m => m.SelectedPlaces)
                    .WithMany()
                    .Map(list =>
                    {
                        list.MapLeftKey("id_user");
                        list.MapRightKey("id_place");
                        list.ToTable("choices");
                    });
        }
    }
}