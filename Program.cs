using Cafe_Colombiano.src.Shared.Context;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Shared.Documentation;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Usuario.Ui;
using Cafe_Colombiano.src.Shared.Helpers;
using System.Linq;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var menu = new MenuPrincipal();
        await menu.Iniciar();   
    }
}