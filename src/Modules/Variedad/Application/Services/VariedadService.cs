using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class VariedadService : IVariedadService
    {
        private readonly IVariedadRepository _variedadRepository;

        public VariedadService(IVariedadRepository variedadRepository)
        {
            _variedadRepository = variedadRepository;
        }

        public async Task<IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>> ConsultarCatalogoAsync()
        {
            return await _variedadRepository.GetAllVariedadesAsync();
        }
    }

}