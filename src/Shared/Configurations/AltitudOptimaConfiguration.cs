using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class AltitudOptimaConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities.AltitudOptima>
    {
        public void Configure(EntityTypeBuilder<AltitudOptima> builder)
        {
            builder.ToTable("AltitudOptima");
            builder.HasKey(ao => ao.id);
            builder.Property(ao => ao.rango_altitud).IsRequired().HasMaxLength(100);
            builder.Property(ao => ao.descripcion);
            builder.HasMany(ao => ao.Variedades)
                .WithOne(v => v.AltitudOptima)
                .HasForeignKey(v => v.id_altitud_optima);
        }
    }
}