using Cafe_Colombiano.src.Shared.Context;


﻿using System.Threading.Tasks;


using Cafe_Colombiano.src.Modules.Variedad.Application.Services;


using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;



using Cafe_Colombiano.src.Shared.Helpers;

using Microsoft.EntityFrameworkCore;

internal class Program
{

    private static async Task Main(string[] args)

    {

        var context = DbContextFactory.Create();





    var repo = new VariedadRepository(context);

    // Assuming VariedadShowCatalogoService expects an IVariedadRepository, not IVariedadService
    // If your repository implements IVariedadRepository, cast or use the interface type

        var verCatalogoService = new CrearVariedadService(repo);

        // Call the method to show the catalog

    await verCatalogoService.CrearVariedad();

    var showCatalogoService = new VariedadShowCatalogoService(repo);
        await showCatalogoService.ShowCatalogo();

        Console.WriteLine("\nPresiona cualquier tecla para salir...");


        Console.ReadKey();

    }

}