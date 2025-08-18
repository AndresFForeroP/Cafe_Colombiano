using Cafe_Colombiano.src.Shared.Context;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Shared.Documentation;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Usuario.Ui;
using Cafe_Colombiano.src.Shared.Helpers;

internal class Program
{
    public static async Task Main(string[] args)
    {
        // var autenticador = new AutenticadorUsuario("admin", "1234");
        // var menu = new DibujoMenusUsuario(autenticador);
        // await menu.Iniciar();


         var context = DbContextFactory.Create();

        var variedadRepository = new VariedadRepository(context);


        var eliminar = new EliminarVariedadService(variedadRepository);


        var actualizar = new ActualizarVariedadService(variedadRepository);

        var crearVariedadService = new CrearVariedadService(variedadRepository);




        await crearVariedadService.CrearVariedad();


        //await eliminar.EliminarVariedadAsync();


   


        //await actualizar.ActualizarVariedad();
    }

}