using System;
using System.Threading;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces; // Interfaz de autenticación
using Cafe_Colombiano.src.Modules.Usuario.Ui; // Módulo de UI
using Spectre.Console; // Librería para interfaces enriquecidas en consola
using SkiaSharp; // Librería para gráficos (ej. mostrar gifs en consola)

/* --- INTERFAZ DE AUTENTICACIÓN --- */
namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{

    // Interfaz que define la autenticación de usuario

    public interface IAutenticadorUsuario
    {
        bool Autenticar(string usuario, string contrasena);
    }

   
    // Implementación concreta de autenticación de usuario

    public class AutenticadorUsuario : IAutenticadorUsuario
    {
        private readonly string _usuarioValido; // Usuario válido
        private readonly string _contrasenaValida; // Contraseña válida

        public AutenticadorUsuario(string usuarioValido, string contrasenaValida)
        {
            _usuarioValido = usuarioValido;
            _contrasenaValida = contrasenaValida;
        }

    
        // Valida si el usuario y contraseña ingresados coinciden con los esperados
        
        public bool Autenticar(string usuario, string contrasena)
        {
            return usuario == _usuarioValido && contrasena == _contrasenaValida;
        }
    }

    /* --- CLASE PRINCIPAL DE MENÚS DE USUARIO --- */
    public class DibujoMenusUsuario
    {
        private readonly IAutenticadorUsuario _autenticador; // Dependencia de autenticación
        private const int MAX_INTENTOS = 3; // Máximo de intentos de login
        public bool Saludos { get; private set; } // Indica si se mostró saludo

        
        // Constructor que permite inyectar un autenticador
        
        public DibujoMenusUsuario(IAutenticadorUsuario autenticador)
        {
            _autenticador = autenticador;
        }

        
        // Constructor por defecto con usuario admin/1234
    
        public DibujoMenusUsuario()
        {
            _autenticador = new AutenticadorUsuario("admin", "1234");
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

       
        // Método principal que inicia la navegación del menú de usuario
       
        public async Task Iniciar()
        {
            MostrarBienvenida(); // <<< Muestra saludo inicial

            int salida = 0;
            do
            {
                await DibujarMenuColorido(); // Dibuja el menú interactivo

                if (!int.TryParse(Console.ReadLine(), out salida) || salida != 1 && salida != 2 && salida != 3 && salida != 9)
                {
                    MostrarMensajeError("Opción no válida, por favor intente de nuevo.");
                }

                if (salida == 9) // Confirmación de salida
                {
                    if (ConfirmarSalida())
                    {
                        MostrarCargaInteractiva("Saliendo del sistema...");
                        Environment.Exit(0);
                    }
                }

            } while (salida != 1 && salida != 2 && salida != 3 && salida != 9);

            await EjecutarOpcionAsync(salida); // Ejecuta la opción seleccionada
        }

        
        // Ejecuta las acciones según la opción elegida
        
        private async Task EjecutarOpcionAsync(int opcion)
        {
            do
            {
                switch (opcion)
                {
                    case 1: // Explorar productos
                        MostrarCargaInteractiva("Abriendo catálogo de productos...");
                        var dibujoExplorarVariedades = new DibujoExplorarVariedades();
                        await dibujoExplorarVariedades.IniciarAsync();
                        break;

                    case 2: // Panel administrativo
                        if (LoginConIntentos()) // Validación de login
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

                    case 3: // Salir
                        if (ConfirmarSalida())
                        {
                            MostrarCargaInteractiva("Saliendo del sistema...");
                            // Ejemplo de gif interactivo en consola
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

      
        //Método para validar login con máximo 3 intentos
        
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
                string contrasena = LeerContrasenaOculta(); // Lectura segura de contraseña

                if (_autenticador.Autenticar(usuario, contrasena))
                {
                    return true; // Acceso permitido
                }
                else
                {
                    intentos++;
                    MostrarMensajeError($"❌ Usuario o contraseña incorrectos. Intentos restantes: {MAX_INTENTOS - intentos}");
                }
            }
            return false; // Bloqueado después de 3 intentos
        }


        // Lectura de contraseña oculta en consola
   
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

       
        // Dibuja el menú principal colorido con Spectre.Console
       
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

            // Menú interactivo
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

            // Ejecutar la opción seleccionada
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

        
        //Muestra un spinner de carga con mensaje
        
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

        
        // Pregunta al usuario si desea confirmar la salida
        
        private bool ConfirmarSalida()
        {
            var confirmacion = AnsiConsole.Confirm("[red]¿Está seguro de que desea salir del sistema?[/]");
            return confirmacion;
        }

        
        // Muestra un mensaje de error en rojo y limpia la pantalla
     
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
