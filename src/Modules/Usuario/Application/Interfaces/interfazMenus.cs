using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces
{
    public interface InterfazMenus
    {
        void Inicio();
        void Dibujar();
        Task ExplorarProductosAsync(int opcion);
        
    }
}