using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Shared.Context;
using Cafe_Colombiano.src.Shared.Documentation;
using Cafe_Colombiano.src.Shared.Helpers;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class MenuExplorarVariedades
    {
        private readonly AppDbContext? _context;

        // Repositorio para acceder a Variedades
        readonly VariedadRepository repo = null!;
        readonly DibujoExplorarVariedades menu;
        readonly VariedadServices serviciosvariedad = new VariedadServices();
        // Constructor: inicializa contexto y repositorio
        public MenuExplorarVariedades()
        {
            var context = DbContextFactory.Create(); // Crear instancia de DbContext
            _context = context;
            repo = new VariedadRepository(context); // Inicializa repositorio
            menu = new DibujoExplorarVariedades();
        }

        // Método principal que ejecuta el explorador
        public async Task IniciarAsync()
        {
            menu.DibujarMenu();
            int salida = AnsiConsole.Prompt(
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
            if (salida == 9)
            {
                AnsiConsole.MarkupLine("[red]👋 Saliendo del explorador...[/]");
                Thread.Sleep(600);
                Console.Clear();
                Environment.Exit(0);
            }
            else
            {
                await ExplorarProductos(salida);
            }
        }

        // Método que ejecuta la acción según la opción elegida
        private async Task ExplorarProductos(int opcion3)
        {
            switch (opcion3)
            {
                case 1: // Ver todos los productos
                    AnsiConsole.MarkupLine("[green]📦 Explorando todos los productos...[/]");
                    await Task.Delay(800);
                    var variedades = await repo.GetAllVariedadesAsync();
                    serviciosvariedad.MostrarVariedades(variedades);
                    break;

                case 2: // Filtrar por variedades
                    AnsiConsole.MarkupLine("[yellow]🔍 Filtrando por variedades...[/]");
                    var _serviceFiltro = new FiltroServices(repo); // Instancia del servicio de filtrado
                    await _serviceFiltro.Filtrar(); // Ejecuta filtrado
                    break;

                case 3: // Exportar catálogo en PDF
                    AnsiConsole.MarkupLine("[blue]📑 Generando catálogo en PDF...[/]");
                    await Task.Delay(800);
                    var context = DbContextFactory.Create();
                    _ = PdfAdministrator.GenerateSamplePdf(context);
                    break;

                case 4: // Volver al menú de usuarios
                    AnsiConsole.MarkupLine("[cyan]↩️  Volviendo al menú de usuarios...[/]");
                    await Task.Delay(800);
                    var menusUsuario = new MenuPrincipal(); // Nueva instancia del menú de usuarios
                    await menusUsuario.Iniciar(); // Llama al menú principal
                    break;
            }
        }
        
    }
}