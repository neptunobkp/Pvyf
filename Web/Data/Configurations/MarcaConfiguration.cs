using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Web.Model;

namespace Web.Data.Configurations
{
    public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(160);

            Seed(builder);
        }

        private void Seed(EntityTypeBuilder<Marca> builder)
        {
        }
    }
}
