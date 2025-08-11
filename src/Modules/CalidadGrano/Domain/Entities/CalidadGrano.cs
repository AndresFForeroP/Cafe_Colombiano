using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities
{
    public class CalidadGrano
    {
        public int id { get; set; }
        public string? nivel_calidad { get; set; }
        public string? descripcion { get; set; }
        public ICollection<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>? Variedades { get; set; }
    }
}