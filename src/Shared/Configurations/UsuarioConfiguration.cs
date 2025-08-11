using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe_Colombiano.src.Shared.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Cafe_Colombiano.src.Modules.Usuario.Domain.Entities.Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(u => u.id);
            builder.Property(u => u.nombre_usuario).IsRequired().HasMaxLength(100);
            builder.Property(u => u.contrasena);
        }
    }
}