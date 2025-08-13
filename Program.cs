using Liga_futbol.Src.Shared.Context;
using Liga_Futbol.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Usuario.Ui;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository;
using System.Threading.Tasks;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        var Saludo = new DibujoMenusUsuario();
        await Saludo.Iniciar();
       
        
    }
}