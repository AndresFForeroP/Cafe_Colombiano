using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities
{
    public class NivelResistencia
    {
        public int ID { get; set; }
        public string? nombre_nivel { get; set; }
        public ICollection<Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities.VariedadResistencia>? VariedadesResistencia { get; set; }
    }
}