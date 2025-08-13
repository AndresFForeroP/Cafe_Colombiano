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
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        var context = DbContextFactory.Create();
        var pdfAdmin = new PdfAdministrator();
        _ = pdfAdmin.GenerateSamplePdf(context);
        var Saludo = new DibujoMenusUsuario();
        await Saludo.Iniciar();
    }
}