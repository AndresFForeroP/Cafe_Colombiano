using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class PotencialRendimientoConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities.PotencialRendimiento>
    {
        public void Configure(EntityTypeBuilder<PotencialRendimiento> builder)
        {
            builder.ToTable("PotencialRendimiento");
            builder.HasKey(pr => pr.id);
            builder.Property(pr => pr.nivel_rendimiento).IsRequired().HasMaxLength(100);
            builder.HasMany(pr => pr.Variedades)
                .WithOne(v => v.PotencialRendimiento)
                .HasForeignKey(v => v.id_potencial_rendimiento);
        }
    }
}