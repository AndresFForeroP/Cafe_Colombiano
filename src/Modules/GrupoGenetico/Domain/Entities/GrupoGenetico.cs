using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities
{
    public class GrupoGenetico
    {
        public int id { get; set; }
        public string? nombre_grupo { get; set; }
        public string? origen { get; set; } 
        public ICollection<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>? Variedades { get; set; }
    }
}