using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class GrupoGeneticoConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities.GrupoGenetico>
    {
        public void Configure(EntityTypeBuilder<GrupoGenetico> builder)
        {
            builder.ToTable("GrupoGenetico");
            builder.HasKey(gc => gc.id);
            builder.Property(gc => gc.nombre_grupo).IsRequired().HasMaxLength(50);
            builder.Property(gc => gc.origen).HasMaxLength(255);
            builder.HasMany(gc => gc.Variedades)
                .WithOne(v => v.GrupoGenetico)
                .HasForeignKey(v => v.id_grupo_genetico);
        }
    }
}