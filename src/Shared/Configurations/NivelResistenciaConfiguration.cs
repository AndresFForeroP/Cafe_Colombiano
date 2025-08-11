using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class NivelResistenciaConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities.NivelResistencia>
    {
        public void Configure(EntityTypeBuilder<NivelResistencia> builder)
        {
            builder.ToTable("NivelResistencia");
            builder.HasKey(nr => nr.id);
            builder.Property(nr => nr.nombre_nivel).IsRequired().HasMaxLength(50);
            builder.HasIndex(nr => nr.nombre_nivel).IsUnique();
            builder.HasMany(nr => nr.VariedadesResistencia)
                .WithOne(v => v.NivelResistencia)
                .HasForeignKey(v => v.id_nivel_resistencia);
        }
    }
}