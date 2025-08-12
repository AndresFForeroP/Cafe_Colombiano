using Liga_futbol.Src.Shared.Context;
using Liga_Futbol.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var context = DbContextFactory.Create();
        var variedadRepository = new VariedadRepository(context); // Implement VariedadRepository if not already done
        var variedadService = new VariedadService(variedadRepository);
        var menuController = new VariedadShowCatalogoService(variedadService);
        await menuController.HandleShowCatalogo();
    }
}