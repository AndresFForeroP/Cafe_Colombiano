using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository
{
    public class VariedadRepository : IVariedadRepository
    {
        private readonly DbContext _context;

        public VariedadRepository(DbContext context)
        {
            _context = context;
        }

        // uqui va el Add,Remove,Update ...etc
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

        public async Task<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad?> GetVariedadByIdAsync(int id)
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
                .FirstOrDefaultAsync(v => v.id == id);
        }

        public void Add(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity)
        {
            _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().Add(entity);
        }

        public void Remove(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity)
        {
            _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().Remove(entity);
        }

        public void Update(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity)
        {
            _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().Update(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }



        public Task ActualizarVariedadAsync(Domain.Entities.Variedad variedad)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>> ConsultarCatalogoAsync()
        {
            return await GetAllVariedadesAsync();
        }

        public Task CrearVariedadAsync(Domain.Entities.Variedad variedad)
        {
            Add(variedad);
            return SaveAsync();
        }

        public Task EliminarVariedadAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}