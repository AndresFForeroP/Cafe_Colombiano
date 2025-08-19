using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class TipoResistenciaConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities.TipoResistencia>
    {
        public void Configure(EntityTypeBuilder<TipoResistencia> builder)
        {
            builder.ToTable("TipoResistencia");
            builder.HasKey(tr => tr.id);
            builder.Property(tr => tr.nombre_tipo).IsRequired().HasMaxLength(100);
            builder.HasIndex(tr => tr.nombre_tipo).IsUnique();
            builder.HasMany(tr => tr.VariedadesResistencia)
                .WithOne(v => v.TipoResistencia)
                .HasForeignKey(v => v.id_tipo_resistencia);
        }
    }
}