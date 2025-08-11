using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities;
using Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities;
using Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities;
using Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities;
using Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities;
using Cafe_Colombiano.src.Modules.Porte.Domain.Entities;
using Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities;
using Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities;
using Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities;
using Cafe_Colombiano.src.Modules.Usuario.Domain.Entities;
using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Liga_futbol.Src.Shared.Context
{
    public class AppDbContext : DbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }
            public DbSet<AltitudOptima> AltitudOptima { get; set; }
            public DbSet<CalidadGrano> CalidadGrano { get; set; }
            public DbSet<GrupoGenetico> GrupoGenetico { get; set; }
            public DbSet<InformacionAgronomica> InformacionAgronomica { get; set; }
            public DbSet<NivelResistencia> NivelResistencia { get; set; }
            public DbSet<Porte> Porte { get; set; }
            public DbSet<PotencialRendimiento> PotencialRendimiento { get; set; }
            public DbSet<TamanoGrano> TamanoGrano { get; set; }
            public DbSet<TipoResistencia> TipoResistencia { get; set; }
            public DbSet<Usuario> Usuario { get; set; }
            public DbSet<Variedad> Variedad { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}