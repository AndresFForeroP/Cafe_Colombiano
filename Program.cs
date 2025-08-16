using Cafe_Colombiano.src.Shared.Context;


﻿using System.Threading.Tasks;


using Cafe_Colombiano.src.Modules.Variedad.Application.Services;


using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;



using Cafe_Colombiano.src.Shared.Helpers;

using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Usuario.Ui;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // Crea el contexto de base de datos
        // Usa tu clase de contexto real

        // Instancia el repositorio

        // Ahora puedes usarlo

        var context = DbContextFactory.Create();
        var variedadRepository = new VariedadRepository(context);
        var eliminar = new EliminarVariedadService(variedadRepository);
        var crearVariedadService = new CrearVariedadService(variedadRepository);

        await crearVariedadService.CrearVariedad();
        await eliminar.EliminarVariedadAsync();
    }

}