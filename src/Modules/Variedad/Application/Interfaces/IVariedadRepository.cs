using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces
{
    public interface IVariedadRepository
    {
        Task<IEnumerable<Domain.Entities.Variedad>> GetAllVariedadesAsync();
    }
}