using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Tatabouf.Domain;

namespace Tatabouf.DAL.Configurations
{
    public class ChoicesConfiguration : EntityTypeConfiguration<Choice>
    {
        public ChoicesConfiguration()
        {
            ToTable("choices");

            Property(t => t.UserId).HasColumnName("id_user");
            Property(t => t.PlaceId).HasColumnName("id_place");
            Property(t => t.OtherIdea).HasColumnName("other_idea");

            HasKey(t => new
            {
                t.UserId,
                t.PlaceId
            });
        }
    }
}
