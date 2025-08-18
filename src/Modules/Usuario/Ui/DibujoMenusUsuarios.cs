using System;
using System.Threading;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces; // Interfaz de autenticación
using Cafe_Colombiano.src.Modules.Usuario.Ui; // Módulo de UI
using Spectre.Console; // Librería para interfaces enriquecidas en consola
using SkiaSharp;
using Cafe_Colombiano.src.Shared.Helpers;
using Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository;
using System.Linq;
using System.Security.Cryptography;
using System.Text; // Librería para gráficos (ej. mostrar gifs en consola)

/* --- INTERFAZ DE AUTENTICACIÓN --- */
namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class DibujoMenusUsuarios()
    {
        // Dibuja el menú principal colorido con Spectre.Console    
        public string DibujarMenuColorido()
        {
            Console.Clear();
            // Encabezado
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║    🌱 Sistema Administrativo Café      ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            // Menú interactivo
            return  AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Seleccione una opción:[/]")
                    .HighlightStyle(Style.Parse("yellow"))
                    .AddChoices(
                        "☕ Explorar productos",
                        "🛠 Panel administrativo",
                        "❌ Salir del sistema"
                    )
            );
            
        }
        //Muestra un spinner de carga con mensaje
        public void MostrarCargaInteractiva(string mensaje)
        {
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots2)
                .SpinnerStyle(Style.Parse("green"))
                .Start(mensaje, ctx =>
                {
                    Thread.Sleep(1500);
                });
        }
        // Muestra un mensaje de error en rojo y limpia la pantalla
        public void MostrarMensajeError(string mensaje)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ResetColor();
            Thread.Sleep(800);
            Console.Clear();
        }
        //Pantalla de bienvenida con ASCII Art usando FigletText de Spectre.Console 
        public void MostrarBienvenida()
        {
            Console.Clear();
            AnsiConsole.Write(
                new FigletText("Cafe Colombiano")
                    .Centered()
                    .Color(Color.Green));

            AnsiConsole.MarkupLine("[bold yellow]🌱 Bienvenido al Sistema Administrativo de Café Colombiano[/]");
            AnsiConsole.MarkupLine("[dim]Presione cualquier tecla para continuar...[/]");
            Console.ReadKey();
        }
    }
}
