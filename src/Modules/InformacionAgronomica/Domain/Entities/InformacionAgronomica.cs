using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities
{
    public class InformacionAgronomica
    {
        public int infomrmacionagronomica { get; set; }
        public string? tiempo_cosecha { get; set; }
        public string? maduracion { get; set; }
        public string? nutricion { get; set; }
        public string? densidad_siembra { get; set; }
        public ICollection<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>? Variedades { get; set; }
    }
}