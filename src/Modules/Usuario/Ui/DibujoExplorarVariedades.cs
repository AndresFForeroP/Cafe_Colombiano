using System;
using System.Threading;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Shared.Context;
using Cafe_Colombiano.src.Shared.Documentation;
using Cafe_Colombiano.src.Shared.Helpers;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class DibujoExplorarVariedades : IDibujoExplorarVariedades
    {
        public void DibujarMenu()
        {
            var rule = new Rule("[bold yellow]☕️ Explorador de Variedades[/]");
            rule.Style = Style.Parse("orange1");
            AnsiConsole.Write(rule);

            // Tabla de opciones
            var table = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.CadetBlue)
                .AddColumn("[bold cyan]#[/]")
                .AddColumn("[bold green]Opción[/]")
                .AddColumn("[bold yellow]Descripción[/]");

            // Agregar filas de opciones
            table.AddRow("1", "📦 Ver productos", "Muestra el catálogo completo de variedades.");
            table.AddRow("2", "🔍 Filtrar variedades", "Busca según tipo de café.");
            table.AddRow("3", "📑 Exportar PDF", "Genera un catálogo profesional en PDF.");
            table.AddRow("4", "↩️  Volver", "Regresa al menú de usuarios.");
            table.AddRow("9", "🚪 Salir", "Cerrar sesión y volver al inicio.");
            AnsiConsole.Write(table);
        } 
    }
}
