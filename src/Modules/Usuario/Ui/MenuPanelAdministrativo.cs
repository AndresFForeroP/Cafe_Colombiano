using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Application.Services;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Modules.Variedad.Ui;
using Cafe_Colombiano.src.Shared.Helpers;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class MenuPanelAdministrativo
    {
        public async Task iniciar()
        {
                int salida = AnsiConsole.Prompt(
                    new SelectionPrompt<int>()
                        .Title("\n[bold cyan]Seleccione una opción:[/]")
                        .HighlightStyle(new Style(Color.Black, Color.Yellow, Decoration.Bold))
                        .PageSize(5)
                        .AddChoices(1, 2, 3, 4) // Opciones del menú
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
                // Llama al método que administra las acciones según la opción seleccionada
                await AdministrarProductosAsync(salida); // repetir hasta que el usuario elija salir
        }
        public async Task AdministrarProductosAsync(int opcion2)
        {
            switch (opcion2)
            {
                case 1:
                    // Cargar módulo de Variedades con barra de progreso
                    AnsiConsole.MarkupLine("[cyan]↩️ Entrando al Gestor de variedades...[/]");
                    Thread.Sleep(500);
                    var MenuGestionVariedades = new MenuGestionVariedades();
                    await MenuGestionVariedades.iniciar();
                    break;

                case 2:
                    // Cargar módulo de Contenido con barra de progreso asíncrona
                    await AnsiConsole.Status()
                        .StartAsync("Cargando módulo...", async ctx =>
                        {
                            ctx.Spinner(Spinner.Known.Star);
                            ctx.Status("[yellow]Entrando al Gestor de contenido...[/]");
                            await Task.Delay(1200);
                        });

                    // ✅ Obtener datos reales de la base de datos
                    try
                    {
                        AnsiConsole.Status()
                            .Start("Cargando variedades...", ctx =>
                            {
                                ctx.Spinner(Spinner.Known.Dots);
                                ctx.Status("[cyan]Obteniendo datos de variedades...[/]");
                            });

                        // Crear contexto y repositorio
                        using var contexto = DbContextFactory.Create();
                        var repo = new VariedadRepository(contexto);

                        // Obtener todas las variedades
                        var variedades = await repo.GetAllVariedadesAsync();

                        Console.Clear();

                        if (variedades?.Any() == true)
                        {
                            AnsiConsole.MarkupLine($"[green]✅ Se cargaron {variedades.Count()} variedades[/]");
                            await Task.Delay(500);
                            var gestor = new GestionServices(repo);
                            await gestor.BuscarYActualizarVariedadAsync();
                        }
                        else
                        {
                            // No hay datos
                            AnsiConsole.MarkupLine("[yellow]⚠️ No se encontraron variedades en la base de datos[/]");
                            AnsiConsole.MarkupLine("[dim]Presione cualquier tecla para continuar...[/]");
                            Console.ReadKey();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores al cargar datos
                        AnsiConsole.MarkupLine($"[red]❌ Error al cargar variedades:[/] {ex.Message}");
                        AnsiConsole.MarkupLine("[dim]Presione cualquier tecla para continuar...[/]");
                        Console.ReadKey();
                    }

                    Console.Clear();
                    break;

                case 3:
                    // Volver al menú de usuarios
                    AnsiConsole.MarkupLine("[cyan]↩️ Volviendo al Menú de Usuarios...[/]");
                    Thread.Sleep(500);
                    Console.Clear();
                    var menusUsuario2 = new MenuPrincipal();
                    await menusUsuario2.Iniciar();
                    break;

                case 4:
                    // Salir del sistema
                    AnsiConsole.MarkupLine("[red]🚪 Cerrando sesión y saliendo del sistema...[/]");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Environment.Exit(0);
                    break;

                default:
                    // Opción inválida
                    AnsiConsole.MarkupLine("[red]⚠️ Opción no válida, por favor intente de nuevo.[/]");
                    Thread.Sleep(500);
                    Console.Clear();
                    break;
            }
        }
    }
}