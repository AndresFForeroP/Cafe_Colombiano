using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities
{
    public class VariedadResistencia
    {
        public int id_variedad { get; set; }
        public int id_tipo_resistencia { get; set; }
        public int id_nivel_resistencia { get; set; }
        public Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities.TipoResistencia? TipoResistencia { get; set; }
        public Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities.NivelResistencia? NivelResistencia { get; set; }
        public Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad? Variedad {get; set;}
    }
}