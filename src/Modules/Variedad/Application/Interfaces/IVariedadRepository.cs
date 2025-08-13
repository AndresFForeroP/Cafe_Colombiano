using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces
{
    public interface IVariedadRepository
    {
        Task<IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>> GetAllVariedadesAsync();
    }
}