using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Cafe_Colombiano.src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using System.Collections.Concurrent;
using Cafe_Colombiano.src.Shared.Helpers;
namespace Cafe_Colombiano.src.Shared.Documentation.PdfTemplates
{
    public class CatalogTemplate : IDocument
    {
        private static readonly Dictionary<string, byte[]> _imageCache = new Dictionary<string, byte[]>();
        public void Compose(IDocumentContainer container)
        {
            throw new NotImplementedException();
        }

        public void CompositionCaratula(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.Green.Lighten1);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.DefaultTextStyle(x => x.FontFamily("verdana"));
                page.Content().Column(column =>
                {
                    column.Item().Text("Catálogo de Variedades de Café Colombiano")
                        .FontColor(Colors.Green.Darken4)
                        .FontSize(30)
                        .ExtraBold()
                        .Underline()
                        .AlignCenter();
                });
            });
        }
        public async Task CompositionBodyAsync(IDocumentContainer container, DbContext context)
        {
            context.Database.EnsureCreated();
            QuestPDF.Settings.License = LicenseType.Community;

            // Obtener datos
            var contexto = DbContextFactory.Create();
            var repo = new VariedadRepository(contexto);
            var variedades = await repo.GetAllVariedadesAsync();
            // Pre-cargar todas las imágenes
            var imagenesVariedades = new Dictionary<int, byte[]>();
            foreach (var variedad in variedades)
            {
                if (!string.IsNullOrEmpty(variedad.imagen_referencia_url))
                {
                    try
                    {
                        var imageBytes = await LoadImageFromUrl(variedad.imagen_referencia_url);
                        imagenesVariedades[variedad.id] = imageBytes;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error cargando imagen para {variedad.nombre_comun}: {ex.Message}");
                    }
                }
            }
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.Green.Lighten3);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.DefaultTextStyle(x => x.FontFamily("verdana"));
                page.Content().Column(column =>
                {
                    column.Item().Row(head =>
                    {
                        head.RelativeItem().Text("Variedad");
                        head.RelativeItem().Text("Descripción");
                    });
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });
                    });
                });
            });
        }
        private static async Task<byte[]> LoadImageFromUrl(string url)
        {
            if (_imageCache.TryGetValue(url, out var cachedImage))
                return cachedImage;

            using var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            
            try
            {
                var imageBytes = await httpClient.GetByteArrayAsync(url);
                _imageCache[url] = imageBytes;
                return imageBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error descargando imagen {url}: {ex.Message}");
                throw;
            }
        }
    }
}