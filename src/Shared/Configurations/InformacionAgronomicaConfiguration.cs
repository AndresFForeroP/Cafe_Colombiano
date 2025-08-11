using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class InformacionAgronomicaConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities.InformacionAgronomica>
    {
        public void Configure(EntityTypeBuilder<InformacionAgronomica> builder)
        {
            builder.ToTable("InformacionAgronomica");
            builder.HasKey(ia => ia.id_variedad);
            builder.Property(ia => ia.tiempo_cosecha).HasMaxLength(255);
            builder.Property(ia => ia.maduracion).HasMaxLength(255);
            builder.Property(ia => ia.nutricion);
            builder.Property(ia => ia.densidad_siembra).HasMaxLength(255);
            builder.HasOne(ia => ia.Variedad)
                .WithOne()
                .HasForeignKey<InformacionAgronomica>(ia => ia.id_variedad)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}