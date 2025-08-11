using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities
{
    public class AltitudOptima
    {
        public int id { get; set; }
        public string? rango_altitud { get; set; }
        public string? description { get; set; }
        public ICollection<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>? Variedades { get; set; }
    }
}