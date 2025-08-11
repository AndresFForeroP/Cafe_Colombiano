using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities;
using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class VariedadConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>
    {
        public void Configure(EntityTypeBuilder<Variedad> builder)
        {
            builder.ToTable("Variedad");
            builder.HasKey(v => v.id);
            builder.Property(v => v.nombre_comun).IsRequired().HasMaxLength(255);
            builder.Property(v => v.nombre_cientifico).IsRequired().HasMaxLength(255);
            builder.Property(v => v.descripcion_general);
            builder.Property(v => v.imagen_referencia_url).HasMaxLength(512);
            builder.Property(v => v.historia_linaje);
            builder.HasOne(v => v.GrupoGenetico)
                .WithMany(gp => gp.Variedades)
                .HasForeignKey(v => v.id_grupo_genetico);
            builder.HasOne(v => v.Porte)
                .WithMany(gp => gp.Variedades)
                .HasForeignKey(v => v.id_porte);
            builder.HasOne(v => v.TamanoGrano)
                .WithMany(gp => gp.Variedades)
                .HasForeignKey(v => v.id_tamano_grano);
            builder.HasOne(v => v.AltitudOptima)
                .WithMany(gp => gp.Variedades)
                .HasForeignKey(v => v.id_altitud_optima);
            builder.HasOne(v => v.PotencialRendimiento)
                .WithMany(gp => gp.Variedades)
                .HasForeignKey(v => v.id_potencial_rendimiento);
            builder.HasOne(v => v.CalidadGrano)
                .WithMany(gp => gp.Variedades)
                .HasForeignKey(v => v.id_calidad_grano);
            builder.HasOne(v => v.InformacionAgronomica)
               .WithOne(ia => ia.Variedad)
               .HasForeignKey<InformacionAgronomica>(ia => ia.id_variedad)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}