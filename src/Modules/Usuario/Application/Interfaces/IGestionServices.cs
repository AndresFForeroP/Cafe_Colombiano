using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces
{
    public interface IGestionServices
    {
        Task<IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>> BuscarYActualizarVariedadAsync();
    }
}