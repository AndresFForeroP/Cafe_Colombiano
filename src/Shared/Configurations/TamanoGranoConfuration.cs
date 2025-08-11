using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class TamanoGranoConfuration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities.TamanoGrano>
    {
        public void Configure(EntityTypeBuilder<TamanoGrano> builder)
        {
            builder.ToTable("TamanoGrano");
            builder.HasKey(tg => tg.id);
            builder.Property(tg => tg.nombre_tamano).IsRequired().HasMaxLength(100);
            builder.HasMany(tg => tg.Variedades)
                .WithOne(v => v.TamanoGrano)
                .HasForeignKey(v => v.id_altitud_optima);
        }
    }
}