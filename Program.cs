using Liga_futbol.Src.Shared.Context;
using Liga_Futbol.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Usuario.Ui;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository;
using System.Threading.Tasks;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        var Saludo = new DibujoMenusUsuario();
        var explorarVariedades = new DibujoExplorarVariedades();
        var AdministrarProductos = new DibujoPanelAdministrativo();
        await Saludo.Iniciar();
        await explorarVariedades.IniciarAsync();
        await AdministrarProductos.Inicio();
        
        
    }
}