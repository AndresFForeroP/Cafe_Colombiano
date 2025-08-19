using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces
{
    public interface IMenuPanelAdministrativo
    {
        Task iniciar();
        Task AdministrarProductosAsync(int opcion2);
    }
}