using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities
{
    public class PotencialRendimiento
    {
        public int id { set; get; }
        public string? nivel_rendimiento { set; get; }
        public ICollection<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>? Variedades { get; set; }
    }
}