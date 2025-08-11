using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Porte.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class PorteConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.Porte.Domain.Entities.Porte>
    {
        public void Configure(EntityTypeBuilder<Porte> builder)
        {
            builder.ToTable("Porte");
            builder.HasKey(p => p.id);
            builder.Property(p => p.nombre_porte).IsRequired().HasMaxLength(50);
            builder.HasIndex(p => p.nombre_porte).IsUnique();
            builder.HasMany(p => p.Variedades)
                .WithOne(v => v.Porte)
                .HasForeignKey(v => v.id_porte);
        }
    }
}