using System;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class DibujoPanelAdministrativo
    {
        public bool Admin { get; private set; }
        private readonly bool usarSpectre;

        public DibujoPanelAdministrativo(bool usarSpectre = true)
        {
            this.usarSpectre = usarSpectre;
        }

        public async Task Inicio()
        {
            int salida = -1;
            do
            {
                if (usarSpectre)
                {
                    Console.Clear();

                    // 🎨 Encabezado bonito
                    var panel = new Panel("[bold cyan]🔐 Panel Administrativo[/]")
                        .Border(BoxBorder.Double)
                        .Header("[white on blue] CAFÉ COLOMBIANO [/]")
                        .Collapse();
                    AnsiConsole.Write(panel);

                    // 📊 Info adicional
                    var tabla = new Table()
                        .Border(TableBorder.Rounded)
                        .AddColumn("[yellow]Módulo[/]")
                        .AddColumn("[green]Descripción[/]");
                    tabla.AddRow("📦 Variedades", "Gestión de cafés y tipos de grano");
                    tabla.AddRow("📝 Contenido", "Gestión de artículos, noticias y catálogos");
                    tabla.AddRow("↩️ Usuarios", "Volver al menú principal");
                    tabla.AddRow("🚪 Salir", "Cerrar el sistema");

                    AnsiConsole.Write(tabla);

                    // 📋 Menú con SelectionPrompt
                    salida = AnsiConsole.Prompt(
                        new SelectionPrompt<int>()
                            .Title("\n[bold cyan]Seleccione una opción:[/]")
                            .HighlightStyle(new Style(Color.Black, Color.Yellow, Decoration.Bold))
                            .PageSize(5)
                            .AddChoices(1, 2, 3, 4)
                            .UseConverter(op =>
                            {
                                return op switch
                                {
                                    1 => "📦 Gestor de Variedades",
                                    2 => "📝 Gestor de Contenido",
                                    3 => "↩️ Volver al Menú de Usuarios",
                                    4 => "🚪 Salir del Sistema",
                                    _ => throw new NotImplementedException()
                                };
                            }));
                }
                else
                {
                    Dibujar();
                    if (!int.TryParse(Console.ReadLine(), out salida))
                    {
                        Console.Clear();
                        Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                        Thread.Sleep(400);
                        Console.Clear();
                        continue;
                    }
                }

                await AdministrarProductosAsync(salida, usarSpectre);

            } while (salida != 4); // repetir hasta salir
        }

        public void Dibujar()
        {
            Console.Clear();
            Console.WriteLine(@"
        ==============================
            Panel Administrativo
        ==============================
        1. Gestor de Variedades
        2. Gestor de Contenido
        3. Volver al Menú de Usuarios
        4. Salir del Sistema
        ==============================
        Seleccione una opción: (1-4): ");
        }

        public static async Task AdministrarProductosAsync(int opcion2, bool usarSpectre)
        {
            switch (opcion2)
            {
                case 1:
                    AnsiConsole.Status()
                        .Start("Cargando módulo...", ctx =>
                        {
                            ctx.Spinner(Spinner.Known.Dots);
                            ctx.Status("[green]Entrando al Gestor de variedades...[/]");
                            Thread.Sleep(1200);
                        });
                    Console.Clear();
                    var menusUsuario1 = new DibujoMenusUsuario();
                    await menusUsuario1.Iniciar();
                    break;

                case 2:
                    AnsiConsole.Status()
                        .Start("Cargando módulo...", ctx =>
                        {
                            ctx.Spinner(Spinner.Known.Star);
                            ctx.Status("[yellow]Entrando al Gestor de contenido...[/]");
                            Thread.Sleep(1200);
                        });
                    Console.Clear();
                    var menusUsuario3 = new DibujoMenusUsuario();
                    await menusUsuario3.Iniciar();
                    break;

                case 3:
                    AnsiConsole.MarkupLine("[cyan]↩️ Volviendo al Menú de Usuarios...[/]");
                    Thread.Sleep(500);
                    Console.Clear();
                    var menusUsuario2 = new DibujoMenusUsuario();
                    await menusUsuario2.Iniciar();
                    break;

                case 4:
                    AnsiConsole.MarkupLine("[red]🚪 Cerrando sesión y saliendo del sistema...[/]");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Environment.Exit(0);
                    break;

                default:
                    AnsiConsole.MarkupLine("[red]⚠️ Opción no válida, por favor intente de nuevo.[/]");
                    Thread.Sleep(500);
                    Console.Clear();
                    break;
            }
        }
    }
}
