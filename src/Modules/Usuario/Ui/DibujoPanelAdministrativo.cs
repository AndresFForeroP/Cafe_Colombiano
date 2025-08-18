using System;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console; // Librería para interfaces de consola enriquecidas
using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities; // Entidad Variedad
using Cafe_Colombiano.src.Shared.Helpers; // Helpers compartidos del proyecto
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository; // Repositorio para acceder a la base de datos

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class DibujoPanelAdministrativo
    {
        // Indica si el usuario tiene permisos de administrador
        public bool Admin { get; private set; }

        // Determina si se usarán las características de Spectre.Console para interfaz enriquecida
        private readonly bool usarSpectre;

        // Constructor, permite definir si usar Spectre
        public DibujoPanelAdministrativo(bool usarSpectre = true)
        {
            this.usarSpectre = usarSpectre;
        }

        
        // Método principal que inicia el panel administrativo y gestiona el flujo de opciones
       
        public async Task Inicio()
        {
            int salida = -1;
            do
            {
                if (usarSpectre)
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

                    // 📋 Menú de selección interactivo
                    salida = AnsiConsole.Prompt(
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
                }
                else
                {
                    // Si no se usa Spectre, dibuja el menú clásico en consola
                    Dibujar();
                    if (!int.TryParse(Console.ReadLine(), out salida))
                    {
                        Console.Clear();
                        Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                        Thread.Sleep(400); // Pausa breve
                        Console.Clear();
                        continue;
                    }
                }

                // Llama al método que administra las acciones según la opción seleccionada
                await AdministrarProductosAsync(salida, usarSpectre);

            } while (salida != 4); // repetir hasta que el usuario elija salir
        }

        
        // Dibuja el menú clásico en consola (sin Spectre)
        
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

        
        // Ejecuta las acciones correspondientes según la opción del menú
      
        public async Task AdministrarProductosAsync(int opcion2, bool usarSpectre)
        {
            switch (opcion2)
            {
                case 1:
                    // Cargar módulo de Variedades con barra de progreso
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
                            
                            // Crear gestor de variedades y llamar al método de búsqueda y actualización
                            using var contextoa = DbContextFactory.Create();
                            var repoa = new VariedadRepository(contextoa);
                            var gestor = new GestorVariedades(repoa);
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
                    var menusUsuario2 = new DibujoMenusUsuario();
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
