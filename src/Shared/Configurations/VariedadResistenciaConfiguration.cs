using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class VariedadResistenciaConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities.VariedadResistencia>
    {
        public void Configure(EntityTypeBuilder<VariedadResistencia> builder)
        {
            builder.ToTable("VariedadResistencia");
            builder.HasKey(vr => new { vr.id_variedad, vr.id_tipo_resistencia });
            builder.HasOne(vr => vr.Variedad)
                .WithMany(v => v.VariedadesResistencia)
                .HasForeignKey(v => v.id_variedad)
                .OnDelete(DeleteBehavior.Cascade);;
            builder.HasOne(vr => vr.TipoResistencia)
                .WithMany(v => v.VariedadesResistencia)
                .HasForeignKey(v => v.id_tipo_resistencia);
            builder.HasOne(vr => vr.NivelResistencia)
                .WithMany(v => v.VariedadesResistencia)
                .HasForeignKey(v => v.id_nivel_resistencia);
        }
    }
}