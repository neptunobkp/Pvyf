using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Model;

namespace Web.Data.Configurations
{
    public class LugarServicioConfiguration : IEntityTypeConfiguration<LugarServicio>
    {
        public void Configure(EntityTypeBuilder<LugarServicio> builder)
        {
            builder.Property(e => e.Lat).HasColumnType("DECIMAL(12,9)");
            builder.Property(e => e.Lng).HasColumnType("DECIMAL(12,9)");
        }
    }
}
