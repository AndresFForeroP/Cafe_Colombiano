using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces
{
    public interface IDibujoMenusUsuarios
    {
        string DibujarMenuColorido();
        void MostrarCargaInteractiva(string mensaje);
        void MostrarMensajeError(string mensaje);
        void MostrarBienvenida();
    }
}