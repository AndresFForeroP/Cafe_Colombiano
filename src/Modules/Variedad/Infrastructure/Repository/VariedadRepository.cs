using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;
using Cafe_Colombiano.src.Modules.Variedad.Ui;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository
{
    public class VariedadRepository : IVariedadRepository
    {
        private DIbujoMenusFiltrar dibujoMenusFiltrar = new DIbujoMenusFiltrar();
        internal readonly DbContext _context;

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
        public async Task Add(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity)
        {
            await _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().AddAsync(entity);
        }
        public async Task Remove(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity)
        {
            _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public void RemoveEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task Update(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity)
        {
            _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public Task ActualizarVariedadAsync(Domain.Entities.Variedad variedad)
        {
            throw new NotImplementedException();
        }
        public int validarentero(int maximo)
        {
            int salida = 0;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out salida) || (salida < 1 || salida > maximo) && salida != 9)
                {
                    Console.WriteLine("VALOR INGRESADO NO VALIDO");
                }
            }
            while (salida < 1 || salida > maximo);
            return salida;
        }
        public string MostrasListaIds()
        {
            var variedades = _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().ToList();

            foreach (var variedad in variedades)
            {
                Console.WriteLine($"ID: {variedad.id}, Nombre: {variedad.nombre_comun} ({variedad.nombre_cientifico})");
            }

            return "Lista de IDs mostrada en consola.";
        }
        public void RemoveEntity(object entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorNombre(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuNombre();
            return Lista.Where(v => v.nombre_cientifico != null &&
                                    v.nombre_cientifico.ToLower().Contains(nombre.ToLower()));
        }

        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorPorte(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuPorte();
            return Lista.Where(v => v.Porte?.nombre_porte != null &&
                                    v.Porte.nombre_porte.ToLower().Contains(nombre.ToLower()));
        }

        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorTamano(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.Menutamano();
            return Lista.Where(v => v.TamanoGrano?.nombre_tamano != null &&
                                    v.TamanoGrano.nombre_tamano.ToLower().Contains(nombre.ToLower()));
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorAltitud( IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuAltitud();
            return Lista.Where(v => v.AltitudOptima?.rango_altitud != null &&
                                    v.AltitudOptima.rango_altitud.ToLower().Contains(nombre.ToLower()));
        }

       public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorRendimiento(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuRendimiento();
            return Lista.Where(v => v.PotencialRendimiento?.nivel_rendimiento != null &&
                                    v.PotencialRendimiento.nivel_rendimiento.ToLower().Contains(nombre.ToLower()));
        }

        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorCalidad(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuCalidad();
            return Lista.Where(v => v.CalidadGrano?.nivel_calidad != null &&
                                    v.CalidadGrano.nivel_calidad.ToLower().Contains(nombre.ToLower()));
        }

       public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorResistencia(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.Menuresistencias();
            string NivelResistencia = dibujoMenusFiltrar.MenuNivelResistencias();
            return Lista.Where(v => v.VariedadesResistencia != null && v.VariedadesResistencia.Any(r =>
                !string.IsNullOrEmpty(r.TipoResistencia?.nombre_tipo) &&
                r.TipoResistencia.nombre_tipo.Contains(nombre, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrEmpty(r.NivelResistencia?.nombre_nivel) &&
                r.NivelResistencia.nombre_nivel.Contains(NivelResistencia, StringComparison.OrdinalIgnoreCase)));
        }

        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorTiempoCoshecha(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuTiempoCosecha();
            return Lista.Where(v => v.InformacionAgronomica?.tiempo_cosecha != null &&
                                    v.InformacionAgronomica.tiempo_cosecha.ToLower().Contains(nombre.ToLower()));
        }

       public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorMaduracion(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuMaduracion();
            return Lista.Where(v => v.InformacionAgronomica?.maduracion != null &&
                                    v.InformacionAgronomica.maduracion.ToLower().Contains(nombre.ToLower()));
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorNutricion(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuNutricion();
            return Lista.Where(v => v.InformacionAgronomica?.nutricion != null &&
                                    v.InformacionAgronomica.nutricion.ToLower().Contains(nombre.ToLower()));
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorDensidad(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuDensidad();
            return Lista.Where(v => v.InformacionAgronomica?.densidad_siembra != null &&
                                    v.InformacionAgronomica.densidad_siembra.ToLower().Contains(nombre.ToLower()));
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorGrupo(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = dibujoMenusFiltrar.MenuGrupo();
            return Lista.Where(v => v.GrupoGenetico?.nombre_grupo != null &&
                                    v.GrupoGenetico.nombre_grupo.ToLower().Contains(nombre.ToLower()));
        }
    }
}