using Cafe_Colombiano.src.Shared.Context;
using Cafe_Colombiano.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Usuario.Ui;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository;
using System.Threading.Tasks;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var autenticador = new AutenticadorUsuario("admin", "12345");
        var menuUsuario = new DibujoMenusUsuario(autenticador);
        await menuUsuario.Iniciar();
    }
}