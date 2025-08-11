using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class CalidadGranoConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities.CalidadGrano>
    {
        public void Configure(EntityTypeBuilder<CalidadGrano> builder)
        {
            builder.ToTable("CalidadGrano");
            builder.HasKey(cg => cg.id);
            builder.Property(cg => cg.nivel_calidad).IsRequired().HasMaxLength(50);
            builder.Property(cg => cg.descripcion);
            builder.HasMany(cg => cg.Variedades)
                .WithOne(v => v.CalidadGrano)
                .HasForeignKey(v => v.id_calidad_grano);
        }
    }
}