using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Liga_futbol.Src.Shared.Context;
using Liga_Futbol.src.Shared.Helpers;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class DibujoMenusUsuario
    {
        public bool Saludos { get; private set; }


        public async Task Iniciar()
        {
            int salida = 0;
            do
            {
                Dibujar();
                if (!int.TryParse(Console.ReadLine(), out salida) || salida != 1 && salida != 2 && salida != 3 && salida != 4 && salida != 5 && salida != 9)
                {
                    Console.Clear();
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    Thread.Sleep(500);
                    Console.Clear();
                }
                if (salida == 9)
                {
                    Console.Clear();
                    Console.WriteLine("Saliendo del sistema...");
                    Thread.Sleep(500);
                    Environment.Exit(0);
                }
            }
            while (salida != 1 && salida != 2 && salida != 3 && salida != 4 && salida != 5 && salida != 9);
            await ExplorarProductosAsync(salida);
        }
        public void Dibujar()
        {
            Console.Clear();
            Console.WriteLine(mensaje);
        }
        public async Task ExplorarProductosAsync(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    var dibujoExplorarVariedades = new DibujoExplorarVariedades();
                    await dibujoExplorarVariedades.IniciarAsync();
                    break;
                case 2:
                    var dibujoPanelAdministrativo = new DibujoPanelAdministrativo();
                    await dibujoPanelAdministrativo.Inicio();
                    break;
                case 3:
                    Console.WriteLine("Saliendo del sistema...");
                    Thread.Sleep(500);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    Thread.Sleep(500);
                    break;
            }
        }
        private string mensaje = """
        ==============================
            Sistema Administrativo 
                Cafe Colombiano
        ==============================
        1. explorar productos
        2. Panel administrativo
        3. Salir
        ==============================
        Ingresa una opción (1-3):
        """;
        
    }
}