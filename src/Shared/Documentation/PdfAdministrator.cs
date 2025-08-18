using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Cafe_Colombiano.src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using System.Collections.Concurrent;
using Cafe_Colombiano.src.Shared.Helpers;
using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities;
using Cafe_Colombiano.src.Shared.Documentation.PdfTemplates;

namespace Cafe_Colombiano.src.Shared.Documentation
{
    public class PdfAdministrator
    {
        private static readonly ConcurrentDictionary<string, byte[]> _imageCache = new();

        public static async Task GenerateSamplePdf(AppDbContext context)
        {
            try
            {
                // Configuración inicial
                context.Database.EnsureCreated();
                QuestPDF.Settings.License = LicenseType.Community;
                
                // Obtener datos
                var repo = new VariedadRepository(context);
                var variedades = await repo.GetAllVariedadesAsync();
                
                // Pre-cargar todas las imágenes ANTES de generar el PDF
                var imagenesVariedades = new Dictionary<int, byte[]>();
                //para entrar a cada variedad y sacar la imagen
                foreach (var variedad in variedades)
                {
                    if (!string.IsNullOrEmpty(variedad.imagen_referencia_url))
                    {
                        try
                        {
                             var imageBytes = await CatalogTemplate.LoadImageFromUrl(variedad.imagen_referencia_url);
                            imagenesVariedades[variedad.id] = imageBytes;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Error cargando imagen para {variedad.nombre_comun}: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ {variedad.nombre_comun} no tiene URL de imagen");
                    }
                }
                // Crear instancia del template
                var catalogTemplate = new CatalogTemplate();
                // Generar documento PDF
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(1, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(12).FontFamily("verdana"));
                        // Encabezado
                        page.Header()
                            .AlignCenter()
                            .Text("Catálogo de Variedades de Café Colombiano")
                            .ExtraBold()
                            .FontSize(24)
                            .FontColor(Colors.Green.Darken2);
                        // Contenido
                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(column =>
                            {
                                column.Spacing(10);
                                foreach (var variedad in variedades)
                                {
                                    // Llamar al método sincronizado
                                    catalogTemplate.TarjetasSync(column.Item(), variedad, imagenesVariedades);
                                }
                            });
                        // Pie de página
                        page.Footer()
                            .AlignRight()
                            .Text(x =>
                            {
                                x.Span("Pg: ");
                                x.CurrentPageNumber();
                            });
                    });
                }).GeneratePdf("catalogo_cafe_colombiano.pdf");
                Console.WriteLine("✅ PDF generado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error generando PDF: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}