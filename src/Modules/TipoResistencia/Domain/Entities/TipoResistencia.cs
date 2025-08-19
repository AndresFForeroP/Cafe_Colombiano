using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities
{
    public class TipoResistencia
    {
        public int id { get; set; }
        public string? nombre_tipo { get; set; }
        public ICollection<Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities.VariedadResistencia>? VariedadesResistencia { get; set; }
    }
}