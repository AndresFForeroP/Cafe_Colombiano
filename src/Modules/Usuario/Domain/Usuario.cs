using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Usuario.Domain
{
    public class Usuario
    {
        public int id { get; set; }
        public string? nombre_usuario { get; set; }
        public string? contrasena { get; set; }
    }
}