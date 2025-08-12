using Cafe_Colombiano.src.Shared.Helpers;
using Liga_futbol.Src.Shared.Context;
using Liga_Futbol.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var context = DbContextFactory.Create();
        var pdfAdmin = new PdfAdministrator();
        // pdfAdmin.GenerateSamplePdf(context);
        pdfAdmin.GenerateSamplePdf();
    }
}