using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Porte.Domain.Entities
{
    public class Porte
    {
        public int id { get; set; }
        public string? nombre_porte { get; set; }
        public ICollection<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>? Variedades { get; set; }
    }
}