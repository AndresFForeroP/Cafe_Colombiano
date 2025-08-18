using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces
{
    public interface IVariedadRepository
    {
        Task<IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>> GetAllVariedadesAsync();
        public Task<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad?> GetVariedadByIdAsync(int id);
        public Task Add(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity);
       
        public Task SaveAsync();
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorNombre(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorPorte(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorTamano(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorAltitud(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorRendimiento(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorCalidad(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorResistencia(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorTiempoCoshecha(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorMaduracion(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorNutricion(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorDensidad(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorGrupo(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista);
        public int validarentero(int maximo);
        public string MostrasListaIds();
        public void RemoveEntity<T>(T entity) where T : class;


        Task<Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities.GrupoGenetico?> GetGrupoGeneticoByNombreAsync(string nombre);
        Task<Cafe_Colombiano.src.Modules.Porte.Domain.Entities.Porte?> GetPorteByNombreAsync(string nombre);
        Task<Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities.TamanoGrano?> GetTamanoGranoByNombreAsync(string nombre);
        Task<Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities.AltitudOptima?> GetAltitudOptimaByRangoAsync(string rango);
        Task<Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities.PotencialRendimiento?> GetPotencialRendimientoByNivelAsync(string nivel);
        Task<Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities.CalidadGrano?> GetCalidadGranoByNivelAsync(string nivel);
        Task<Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities.TipoResistencia?> GetTipoResistenciaByNombreAsync(string nombre);
        Task<Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities.NivelResistencia?> GetNivelResistenciaByNombreAsync(string nombre);


    }
}