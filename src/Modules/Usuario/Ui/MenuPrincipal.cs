using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository;
using Cafe_Colombiano.src.Shared.Helpers;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class MenuPrincipal
    {
        public readonly UsuarioRepository repo = null!;
        private readonly DibujoMenusUsuarios dibujoMenusPrincipal;
        public MenuPrincipal()
        {
            var context = DbContextFactory.Create(); // Crear instancia de DbContext
            repo = new UsuarioRepository(context);
            dibujoMenusPrincipal = new DibujoMenusUsuarios();
        }
        // Método principal que inicia la navegación del menú de usuario
        public async Task Iniciar()
        {
            dibujoMenusPrincipal.MostrarBienvenida();
            string opcionSeleccionada = dibujoMenusPrincipal.DibujarMenuColorido();
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
        // Ejecuta las acciones según la opción elegida
        private async Task EjecutarOpcionAsync(int opcion)
        {
            do
            {
                switch (opcion)
                {
                    case 1: // Explorar productos
                        dibujoMenusPrincipal.MostrarCargaInteractiva("Abriendo catálogo de productos...");
                        var dibujoExplorarVariedades = new MenuExplorarVariedades();
                        await dibujoExplorarVariedades.IniciarAsync();
                        break;

                    case 2: // Panel administrativo
                        Console.Clear();
                        if (await LoginConIntentos())
                        {
                            dibujoMenusPrincipal.MostrarCargaInteractiva("Accediendo al Panel Administrativo...");
                            var dibujoPanelAdministrativo = new MenuPanelAdministrativo();
                            await dibujoPanelAdministrativo.iniciar();
                        }
                        else
                        {
                            dibujoMenusPrincipal.MostrarMensajeError($"❌ Demasiados intentos fallidos");
                            Thread.Sleep(5000);
                            dibujoMenusPrincipal.MostrarCargaInteractiva("Saliendo del sistema...");
                            Environment.Exit(0);
                        }
                        break;

                    case 3: // Salir
                        if (ConfirmarSalida())
                        {
                            dibujoMenusPrincipal.MostrarCargaInteractiva("Saliendo del sistema...");
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        dibujoMenusPrincipal.MostrarMensajeError("Opción no válida, por favor intente de nuevo.");
                        break;
                }
            } while (opcion != 3);
        }
        private async Task<bool> LoginConIntentos()
        {
            int MAX_INTENTOS = 3;
            int intentos = 0;

            while (intentos < MAX_INTENTOS)
            {
                var usuarios = await repo.GetUserAsync();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== 🔒 Autenticación de Usuario (Panel Administrativo) ===");
                Console.ResetColor();
                Console.Write("Usuario: ");
                string usuario = Console.ReadLine() ?? "";
                Console.Write("Contraseña: ");
                string contrasenaIngresada = LeerContrasenaOculta();
                string contrasena = HashSHA256(contrasenaIngresada); // Lectura segura de contraseña
                if (usuarios.Any(u => u.nombre_usuario == usuario && u.contrasena == contrasena))
                {
                    return true; // Acceso permitido
                }
                else
                {
                    intentos++;
                    dibujoMenusPrincipal.MostrarMensajeError($"❌ Usuario o contraseña incorrectos. Intentos restantes: {MAX_INTENTOS - intentos}");
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
        private bool ConfirmarSalida()
        {
            var confirmacion = AnsiConsole.Confirm("[red]¿Está seguro de que desea salir del sistema?[/]");
            return confirmacion;
        }
        private string HashSHA256(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower(); // Igual que en MySQL
        }
    }   
}
            