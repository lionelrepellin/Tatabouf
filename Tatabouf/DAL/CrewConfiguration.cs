using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Tatabouf.Domain;

namespace Tatabouf.DAL
{
    public class CrewConfiguration : EntityTypeConfiguration<Crew>
    {
        public CrewConfiguration()
        {
            ToTable("equipage");
            HasKey(t => t.Id);

            Property(t => t.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).HasColumnName("nom");
            Property(t => t.InscriptionDate).HasColumnName("date_inscription");
                        
            Property(t => t.MarieBlachere).HasColumnName("marie_blachere");
            Property(t => t.Carrefour).HasColumnName("carrefour");
            Property(t => t.Kebab).HasColumnName("kebab");
            Property(t => t.Quick).HasColumnName("quick");
            Property(t => t.Other).HasColumnName("autre");

            Property(t => t.NumberOfSeatsAvailable).HasColumnName("nb_places_dispo");
        }
    }
}