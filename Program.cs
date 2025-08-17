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
        var context = DbContextFactory.Create();
        var pdfAdmin = new PdfAdministrator();
        _ = PdfAdministrator.GenerateSamplePdf(context);
        var autenticador = new AutenticadorUsuario("admin", "12345");
        var menuUsuario = new DibujoMenusUsuario(autenticador);
        await menuUsuario.Iniciar();
    }

}