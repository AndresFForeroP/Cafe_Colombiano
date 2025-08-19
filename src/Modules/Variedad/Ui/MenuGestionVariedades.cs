using System;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Shared.Helpers;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class MenuGestionVariedades : IMenuGestionVariedades
    {
        private readonly VariedadRepository repo = null!;
        readonly VariedadServices serviciosvariedad = new VariedadServices();
        public MenuGestionVariedades()
        {
            var context = DbContextFactory.Create();
            repo = new VariedadRepository(context);
        }
        public async Task iniciar()
        {
            int salida = AnsiConsole.Prompt(
                        new SelectionPrompt<int>()
                            .Title("\n[bold cyan]Seleccione una opción:[/]")
                            .HighlightStyle(new Style(Color.Black, Color.Yellow, Decoration.Bold))
                            .PageSize(7)
                            .AddChoices(1, 2, 3, 4, 5, 6) // Opciones del menú
                            .UseConverter(op =>
                            {
                                return op switch
                                {
                                    1 => "📦 Crear Nueva Variedad",
                                    2 => "📝 Ver Variedades",
                                    3 => "↩️ Actualizar Variedad",
                                    4 => "❌ Eliminar Variedad",
                                    5 => "↩️ Volver al Menú de Administrador",
                                    6 => "🚪 Salir del Sistema",
                                    _ => throw new NotImplementedException()
                                };
                            }));
            await AdministrarProductosAsync(salida);
        }
        public async Task AdministrarProductosAsync(int opcion2)
        {
            var menusUsuario2 = new MenuPanelAdministrativo();
            switch (opcion2)
            {
                case 1:
                    AnsiConsole.MarkupLine("[green] Entrando a crear nueva variedad...[/]");
                    await Task.Delay(800);
                    var CrearVariedadService = new CrearVariedadService(repo);
                    await CrearVariedadService.CrearVariedad();
                    break;

                case 2:
                    AnsiConsole.MarkupLine("[purple]📦 Explorando todos los productos...[/]");
                    await Task.Delay(800);
                    var variedades = await repo.GetAllVariedadesAsync();
                    serviciosvariedad.MostrarVariedades(variedades);
                    break;

                case 3:
                    AnsiConsole.MarkupLine("[green] Entrando a actualizar variedad...[/]");
                    await Task.Delay(800);
                    var ActualizarVariedadService = new ActualizarVariedadService(repo);
                    await ActualizarVariedadService.ActualizarVariedad();
                    break;

                case 4:
                    AnsiConsole.MarkupLine("[green] Entrando a actualizar variedad...[/]");
                    await Task.Delay(800);
                    var EliminarVariedadService = new EliminarVariedadService(repo);
                    await EliminarVariedadService.EliminarVariedadAsync();
                    break;
                case 5:
                    // Volver al menú de usuarios
                    AnsiConsole.MarkupLine("[cyan]↩️ Volviendo al Menú de Administrador...[/]");
                    Thread.Sleep(500);
                    Console.Clear();
                    await menusUsuario2.iniciar();
                    break;

                case 6:
                    // Salir del sistema
                    AnsiConsole.MarkupLine("[red]🚪 Cerrando sesión y saliendo del sistema...[/]");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
            
            await iniciar();
        }

    }
    
}