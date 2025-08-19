using System;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console; // Librería para interfaces de consola enriquecidas
using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities; // Entidad Variedad
using Cafe_Colombiano.src.Shared.Helpers; // Helpers compartidos del proyecto
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces; // Repositorio para acceder a la base de datos

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class DibujoPanelAdministrativo : IDibujoPanelAdministrativo
    {
        public void Dibujar()
        {
            Console.Clear();
            // 🎨 Encabezado bonito usando Panel de Spectre.Console
            var panel = new Panel("[bold cyan]🔐 Panel Administrativo[/]")
                .Border(BoxBorder.Double) // Borde doble
                .Header("[white on blue] CAFÉ COLOMBIANO [/]")
                .Collapse(); // Colapsa el contenido si es muy largo
            AnsiConsole.Write(panel);

            // 📊 Tabla con información de módulos
            var tabla = new Table()
                .Border(TableBorder.Rounded) // Borde redondeado
                .AddColumn("[yellow]Módulo[/]")
                .AddColumn("[green]Descripción[/]");
            tabla.AddRow("📦 Variedades", "Gestión de cafés y tipos de grano");
            tabla.AddRow("📝 Contenido", "Gestión de artículos, noticias y catálogos");
            tabla.AddRow("↩️ Usuarios", "Volver al menú principal");
            tabla.AddRow("🚪 Salir", "Cerrar el sistema");
            AnsiConsole.Write(tabla);
        } 
    }
}
