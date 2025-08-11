using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities
{
    public class TamanoGrano
    {
        public int id { get; set; }
        public string? nombre_tamano { get; set; }
        public ICollection<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>? Variedades { get; set; }
    }
}