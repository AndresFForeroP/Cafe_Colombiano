using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Shared.Helpers;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Cafe_Colombiano.src.Shared.Context;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public class DibujoExplorarVariedades
    {

        private readonly AppDbContext? _context;
        private readonly Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository.ExplorarVrepository repo = null!;
        public bool Saludo { get; private set; }
        
        // Constructores que ameriten inyección de dependencias
        public DibujoExplorarVariedades()
        {
            var context = DbContextFactory.Create();
            _context = context;
            repo = new Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository.ExplorarVrepository(context);
        }

        public async Task IniciarAsync()
        {
            int salida = 0;
            do
            {
                Dibujar();
                Console.WriteLine("Seleccione una opción:");
                if (!int.TryParse(Console.ReadLine(), out salida) || salida != 1 && salida! != 2 && salida != 3 && salida != 4 && salida != 9)
                {
                    Console.Clear();
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                if (salida == 1 || salida == 2 || salida == 3 || salida == 4)
                {
                    Console.WriteLine("1");
                    ExplorarProductos(salida);
                }
            }
            while (salida != 9);
            Console.Clear();
            Console.WriteLine("Volviendo al menú de usuarios...");
            Thread.Sleep(2000);
            Console.Clear();
            var menusUsuario = new DibujoMenusUsuario();
            await menusUsuario.Iniciar();
        }
        public void Dibujar()
        {
            Console.Clear();
            Console.WriteLine(Mensaje);
        }
        public static void ExplorarProductos(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Explorando productos...");
                    Thread.Sleep(500);
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("ingresate a filtros por variedades...");
                    Thread.Sleep(500);
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Generar catálogo de productos en PDF...");
                    Thread.Sleep(500);
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Volviendo al menú de usuarios...");
                    Thread.Sleep(500);
                    var menusUsuario = new DibujoMenusUsuario();
                    _ = menusUsuario.Iniciar();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opción no válida, por favor intente de nuevo.");
                    Thread.Sleep(500);
                    Console.Clear();
                    break;
            }


        }
        private string Mensaje = """
        ==============================
            Explorar Productos
        ==============================
        1. Ver todos los productos
        2. Filtrar por variedades
        3. Generar catálogo de productos en PDF
        4. Volver al menú de usuarios
        ==============================
        """;
    }
}
