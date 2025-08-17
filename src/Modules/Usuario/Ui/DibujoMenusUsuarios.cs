using System;
using System.Threading;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Usuario.Ui;
using Spectre.Console;
using SkiaSharp;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public interface IAutenticadorUsuario
    {
        bool Autenticar(string usuario, string contrasena);
    }

    public class AutenticadorUsuario : IAutenticadorUsuario
    {
        private readonly string _usuarioValido;
        private readonly string _contrasenaValida;

        public AutenticadorUsuario(string usuarioValido, string contrasenaValida)
        {
            _usuarioValido = usuarioValido;
            _contrasenaValida = contrasenaValida;
        }

        public bool Autenticar(string usuario, string contrasena)
        {
            return usuario == _usuarioValido && contrasena == _contrasenaValida;
        }
    }
    

    public class DibujoMenusUsuario
    {
        private readonly IAutenticadorUsuario _autenticador;
        private const int MAX_INTENTOS = 3;
        public bool Saludos { get; private set; }

        public DibujoMenusUsuario(IAutenticadorUsuario autenticador)
        {
            _autenticador = autenticador;
        }

        public DibujoMenusUsuario()
        {
            _autenticador = new AutenticadorUsuario("admin", "1234");
        }

        // === Pantalla de bienvenida con ASCII Art ===
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

        public async Task Iniciar()
        {
            MostrarBienvenida(); // <<< Nueva bienvenida

            int salida = 0;
            do
            {
                await DibujarMenuColorido();
                if (!int.TryParse(Console.ReadLine(), out salida) || salida != 1 && salida != 2 && salida != 3 && salida != 9)
                {
                    MostrarMensajeError("Opción no válida, por favor intente de nuevo.");
                }
                if (salida == 9)
                {
                    if (ConfirmarSalida())
                    {
                        MostrarCargaInteractiva("Saliendo del sistema...");
                        Environment.Exit(0);
                    }
                }
            }
            while (salida != 1 && salida != 2 && salida != 3 && salida != 9);

            await EjecutarOpcionAsync(salida);
        }

        private async Task EjecutarOpcionAsync(int opcion)
        {
            do
            {
                switch (opcion)
                {
                    case 1:
                        MostrarCargaInteractiva("Abriendo catálogo de productos...");
                        var dibujoExplorarVariedades = new DibujoExplorarVariedades();
                        await dibujoExplorarVariedades.IniciarAsync();
                        break;

                    case 2:
                        if (LoginConIntentos())
                        {
                            Console.Clear();
                            MostrarCargaInteractiva("Accediendo al Panel Administrativo...");
                            var dibujoPanelAdministrativo = new DibujoPanelAdministrativo();
                            await dibujoPanelAdministrativo.Inicio();
                        }
                        else
                        {
                            MostrarMensajeError("🚫 Acceso bloqueado. Demasiados intentos fallidos.");
                            MostrarMensajeError("SALIENDO DEL SISTEMA POR SEGURIDAD");
                            Environment.Exit(0);
                        }
                        break;

                    case 3:
                        if (ConfirmarSalida())
                        {
                            MostrarCargaInteractiva("Saliendo del sistema...");
                            GifRenderer.MostrarGifEnConsola("/home/camper/Cafe_Colombiano/video/fall-kirby.gif");
                            Environment.Exit(0);
                        }
                        break;

                    default:
                        MostrarMensajeError("Opción no válida, por favor intente de nuevo.");
                        break;
                }
            } while (opcion != 3);
        }

        private bool LoginConIntentos()
        {
            const int MAX_INTENTOS = 3;
            int intentos = 0;

            while (intentos < MAX_INTENTOS)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== 🔒 Autenticación de Usuario (Panel Administrativo) ===");
                Console.ResetColor();

                Console.Write("Usuario: ");
                string usuario = Console.ReadLine();

                Console.Write("Contraseña: ");
                string contrasena = LeerContrasenaOculta();

                if (_autenticador.Autenticar(usuario, contrasena))
                {
                    return true;
                }
                else
                {
                    intentos++;
                    MostrarMensajeError($"❌ Usuario o contraseña incorrectos. Intentos restantes: {MAX_INTENTOS - intentos}");
                }
            }
            return false; // Bloqueado después de 3 intentos
        }

        private string LeerContrasenaOculta()
        {
            string contrasena = "";
            ConsoleKeyInfo tecla;
            do
            {
                tecla = Console.ReadKey(true);
                if (tecla.Key != ConsoleKey.Backspace && tecla.Key != ConsoleKey.Enter)
                {
                    contrasena += tecla.KeyChar;
                    Console.Write("*");
                }
                else if (tecla.Key == ConsoleKey.Backspace && contrasena.Length > 0)
                {
                    contrasena = contrasena.Substring(0, contrasena.Length - 1);
                    Console.Write("\b \b");
                }
            } while (tecla.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return contrasena;
        }

        public async Task DibujarMenuColorido()
        {
            Console.Clear();
            // Encabezado
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║    🌱 Sistema Administrativo Café      ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            var opcionSeleccionada = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Seleccione una opción:[/]")
                    .HighlightStyle(Style.Parse("yellow"))
                    .AddChoices(
                        "☕ Explorar productos",
                        "🛠 Panel administrativo",
                        "❌ Salir del sistema"
                    )
            );

            switch (opcionSeleccionada)
            {
                case "☕ Explorar productos":
                    await EjecutarOpcionAsync(1);
                    break;
                case "🛠 Panel administrativo":
                    await EjecutarOpcionAsync(2);
                    break;
                case "❌ Salir del sistema":
                    await EjecutarOpcionAsync(3);
                    break;
            }
        }

        private void MostrarCargaInteractiva(string mensaje)
        {
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots2)
                .SpinnerStyle(Style.Parse("green"))
                .Start(mensaje, ctx =>
                {
                    Thread.Sleep(1500);
                });
        }


        private bool ConfirmarSalida()
        {
            var confirmacion = AnsiConsole.Confirm("[red]¿Está seguro de que desea salir del sistema?[/]");
            return confirmacion;
        }

        private void MostrarMensajeError(string mensaje)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ResetColor();
            Thread.Sleep(800);
            Console.Clear();
        }
    }
}
