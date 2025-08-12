using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class DibujoPanelAdministrativo
    {
        public bool Admin { get; private set; }


        public async Task Inicio()
        {
            int salida = 0;
            do
            {
                Dibujar();
                if (!int.TryParse(Console.ReadLine(), out salida) || salida != 1 && salida != 2 && salida != 3 && salida != 4 && salida != 5 && salida != 9)
                {
                    Console.Clear();
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                if (salida == 9)
                {
                    Console.Clear();
                    Console.WriteLine("Saliendo del sistema...");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                }
            }
            while (salida != 1 && salida != 2 && salida != 3 && salida != 4 && salida != 5 && salida != 9);
            AdministrarProductos(salida);
        }
        public void Dibujar()
        {
            Console.Clear();
            Console.WriteLine(Message);
        }
        public static void AdministrarProductos(int opcion2)
        {
            switch (opcion2)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("entrando al Gestor de variedades...");
                    Thread.Sleep(500);
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("entrando al Gestor de contenido...");
                    Thread.Sleep(500);
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Volviendo al Menu de Usuarios...");
                    Thread.Sleep(500);
                    //var menusUsuario = new DibujoMenusUsuario();
                    //await menusUsuario.Iniciar();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Saliendo del sistema...");
                    Thread.Sleep(500);
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    Thread.Sleep(500);
                    Console.Clear();
                    break;

            }
        }
        private string Message = """
        ==============================
            Panel Administrativo
        ==============================
        1. Gestor de Variedades
        2. Gestor de Contenido
        3. Volver al Menú de Usuarios
        4. Salir del Sistema
        ==============================
        Seleccione una opción: (1-4):
        """;
    }
}