using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces
{
    public interface IVariedadRepository
    {
        Task<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad?> GetVariedadByIdAsync(int id);

        Task<IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>> GetAllVariedadesAsync();
        public Task<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad?> GetVariedadByIdAsync(int id);
        public void Add(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity);
        public void Remove(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity);
        public void Update(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity);
        public Task SaveAsync();
        public Task ActualizarVariedadAsync(Domain.Entities.Variedad variedad);
        public Task EliminarVariedadAsync(int id);
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
    }
}