using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository
{
    public class VariedadRepository :IVariedadRepository
    {
        private readonly DbContext _context;

        public VariedadRepository(DbContext context)
        {
            _context = context;
        }
        

        public async Task<IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>> GetAllVariedadesAsync()
        {
            return await _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>()
                .Include(v => v.AltitudOptima)
                .Include(v => v.GrupoGenetico)
                .Include(v => v.Porte)
                .Include(v => v.TamanoGrano)
                .Include(v => v.PotencialRendimiento)
                .Include(v => v.CalidadGrano)
                .Include(v => v.InformacionAgronomica)
                .Include(v => v.VariedadesResistencia!)
                    .ThenInclude(vr => vr.NivelResistencia)
                .Include(v => v.VariedadesResistencia!)
                    .ThenInclude(vr => vr.TipoResistencia)
                .ToListAsync();
        }
    }
}