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

        void Add(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity);
        void Remove(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity);
        void Update(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity);
        Task SaveAsync();

    }
}