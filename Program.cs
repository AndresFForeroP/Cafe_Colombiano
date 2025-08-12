using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Liga_futbol.Src.Shared.Context;
using Liga_Futbol.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

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