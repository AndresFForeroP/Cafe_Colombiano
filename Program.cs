using Cafe_Colombiano.src.Shared.Context;
using Cafe_Colombiano.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Usuario.Ui;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;

internal class Program
{
     private static async Task Main(string[] args)
    {
        var context = DbContextFactory.Create();

        var repo = new VariedadRepository(context);
        var filtroServices = new FiltroServices(repo);

        await filtroServices.Filtrar();
        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}