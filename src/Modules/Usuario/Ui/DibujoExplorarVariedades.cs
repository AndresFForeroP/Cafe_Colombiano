using System;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class DibujoExplorarVariedades
    {
        public async Task IniciarAsync()
        {
            int salida = 0;
            do
            {
                Console.Clear();
                var rule = new Rule("[bold yellow]☕️ Explorador de Variedades[/]");
                rule.Style = Style.Parse("orange1");
                AnsiConsole.Write(rule);
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .BorderColor(Color.CadetBlue)
                    .AddColumn("[bold cyan]#[/]")
                    .AddColumn("[bold green]Opción[/]")
                    .AddColumn("[bold yellow]Descripción[/]");
                table.AddRow("1", "📦 Ver productos", "Muestra el catálogo completo de variedades.");
                table.AddRow("2", "🔍 Filtrar variedades", "Busca según tipo de café.");
                table.AddRow("3", "📑 Exportar PDF", "Genera un catálogo profesional en PDF.");
                table.AddRow("4", "↩️  Volver", "Regresa al menú de usuarios.");
                table.AddRow("9", "🚪 Salir", "Cerrar sesión y volver al inicio.");
                AnsiConsole.Write(table);
                salida = AnsiConsole.Prompt(
                    new SelectionPrompt<int>()
                        .Title("[bold cyan]Elige una opción:[/]")
                        .PageSize(6)
                        .AddChoices(1, 2, 3, 4, 9)
                        .UseConverter(op =>
                        {
                            return op switch
                            {
                                1 => "📦 Ver todos los productos",
                                2 => "🔍 Filtrar por variedades",
                                3 => "📑 Generar catálogo PDF",
                                4 => "↩️  Volver al menú de usuarios",
                                9 => "🚪 Salir",
                                _ => throw new NotImplementedException()
                            };
                        }));

                if (salida != 9)
                    await ExplorarProductos(salida);

            } while (salida != 9);

            AnsiConsole.MarkupLine("[red]👋 Saliendo del explorador...[/]");
            Thread.Sleep(600);
            Console.Clear();
            Environment.Exit(0);
        }
        public static async Task ExplorarProductos(int opcion3)
        {
            switch (opcion3)
            {
                case 1:
                    AnsiConsole.MarkupLine("[green]📦 Explorando todos los productos...[/]");
                    await Task.Delay(800);
                    break;

                case 2:
                    AnsiConsole.MarkupLine("[yellow]🔍 Filtrando por variedades...[/]");
                    await Task.Delay(800);
                    break;

                case 3:
                    AnsiConsole.MarkupLine("[blue]📑 Generando catálogo en PDF...[/]");
                    await Task.Delay(800);
                    break;

                case 4:
                    AnsiConsole.MarkupLine("[cyan]↩️  Volviendo al menú de usuarios...[/]");
                    await Task.Delay(800);
                    var menusUsuario = new DibujoMenusUsuario();
                    await menusUsuario.Iniciar();
                    break;
            }
        }
    }
}
