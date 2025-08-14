using System;
using System.Threading;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Usuario.Ui;
using Spectre.Console;

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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public DibujoMenusUsuario()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
        }

        public async Task Iniciar()
        {
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
                    MostrarCargando("Saliendo del sistema");
                    Environment.Exit(0);
                }
            }
            while (salida != 1 && salida != 2 && salida != 3 && salida != 9);

            await EjecutarOpcionAsync(salida);
        }

        private async Task EjecutarOpcionAsync(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    MostrarCargando("Abriendo catálogo de productos");
                    var dibujoExplorarVariedades = new DibujoExplorarVariedades();
                    await dibujoExplorarVariedades.IniciarAsync();
                    break;

                case 2:
                    if (LoginConIntentos())
                    {
                        MostrarCargando("Accediendo al Panel Administrativo");
                        var dibujoPanelAdministrativo = new DibujoPanelAdministrativo();
                        dibujoPanelAdministrativo.Inicio();
                    }
                    else
                    {
                        MostrarMensajeError("🚫 Acceso bloqueado. Demasiados intentos fallidos.");
                    }
                    break;

                case 3:
                    MostrarCargando("Saliendo del sistema");
                    Environment.Exit(0);
                    break;

                default:
                    MostrarMensajeError("Opción no válida, por favor intente de nuevo.");
                    break;
            }
        }

        private bool LoginConIntentos()
        {
            int intentos = 0;

            while (intentos < MAX_INTENTOS)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== 🔒 Autenticación de Usuario (Panel Administrativo) ===");
                Console.ResetColor();

                Console.Write("Usuario: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string usuario = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

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

    // Menú interactivo con Spectre.Console
    var opcionSeleccionada = Spectre.Console.AnsiConsole.Prompt(
        new Spectre.Console.SelectionPrompt<string>()
            .Title("[cyan]Seleccione una opción:[/]")
            .HighlightStyle(Spectre.Console.Style.Parse("yellow"))
            .AddChoices(
                "☕ Explorar productos",
                "🛠 Panel administrativo",
                "❌ Salir del sistema"
            )
    );

    // Mapear las opciones seleccionadas a números
    switch (opcionSeleccionada)
    {
        case "☕ Explorar productos":
            await EjecutarOpcionAsync(1);
            break;
        case "🛠 Panel administrativo":
            await EjecutarOpcionAsync(2);
            break;
        case "❌ Salir del sistema":
            await EjecutarOpcionAsync(9);
            break;
    }
}

        private void MostrarImagenEnConsola(string v1, int v2)
        {
            throw new NotImplementedException();
        }

        private void MostrarCargando(string mensaje)
        {
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(mensaje);
            Console.ResetColor();
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(300);
                Console.Write(".");
            }
            Console.WriteLine();
            Thread.Sleep(300);
        }

        private void MostrarMensajeError(string mensaje)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ResetColor();
            Thread.Sleep(400);
            Console.Clear();
        }
    }
}