using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces
{
    public interface IVariedadService 
    {
        Task CrearVariedadAsync(Domain.Entities.Variedad variedad);
        Task ActualizarVariedadAsync(Domain.Entities.Variedad variedad);
        Task EliminarVariedadAsync(int id);
        Task<IEnumerable<Domain.Entities.Variedad>> ConsultarCatalogoAsync();
    }
}