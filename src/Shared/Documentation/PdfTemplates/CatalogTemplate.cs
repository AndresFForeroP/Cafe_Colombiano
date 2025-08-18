using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Cafe_Colombiano.src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using System.Collections.Concurrent;
using Cafe_Colombiano.src.Shared.Helpers;
using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities;
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
        public async Task Tarjetas(IContainer component, Variedad variedad,
            Dictionary<int, byte[]> imagenesVariedades)
        {
            component.Background(Colors.Green.Lighten5)
                .CornerRadius(10)
                .Border(1)
                .BorderColor(Colors.Green.Darken2)
                .Padding(20)
                .Column(variedadColumn =>
                {
                    // Fila con imagen y nombre
                    variedadColumn.Item().Row(row =>
                    {
                        // Imagen
                        if (imagenesVariedades.TryGetValue(variedad.id, out var imagenBytes))
                        {
                            _ = row.RelativeItem()
                                .MinWidth(120)
                                .MaxWidth(150)
                                .MinHeight(110)
                                .MaxHeight(150)
                                .AlignCenter()
                                .Padding(10)
                                .Border(1)
                                .BorderColor(Colors.Green.Darken2)
                                .CornerRadius(10)
                                .Image(imagenBytes).FitArea();
                        }
                        else
                        {
                            row.RelativeItem().Height(100).Width(100)
                                .Column(col =>
                                {
                                    col.Item()
                                        .Image(Placeholders.Image);
                                    col.Item().PaddingTop(5)
                                        .Text("Imagen no disponible")
                                        .FontSize(8);
                                    col.Item().Border(1).BorderColor(Colors.Grey.Lighten1);
                                });
                        }

                        // Nombre
                        row.RelativeItem()
                            .Text($"{variedad.nombre_comun} ({variedad.nombre_cientifico})")
                            .ExtraBold()
                            .FontSize(24)
                            .FontColor(Colors.Green.Darken4)
                            .AlignRight();
                    });

                    // Detalles principales
                    variedadColumn.Item().PaddingTop(5).Row(row =>
                    {
                        variedadColumn.Item().PaddingTop(10).CornerRadius(10).Column(detailsColumn =>
                        {
                            detailsColumn.Item().Table(table1 =>
                            {
                                table1.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table1.Cell().Row(1).Column(1).Background(Colors.Green.Lighten2).Padding(5)
                                    .Text("Potencial de rendimiento:").SemiBold();
                                table1.Cell().Row(1).Column(2).Background(Colors.Green.Lighten2).Padding(5)
                                    .Text(variedad.PotencialRendimiento?.nivel_rendimiento ?? "N/A");

                                table1.Cell().Row(2).Column(1).Background(Colors.Green.Lighten3).Padding(5)
                                    .Text("Calidad de grano:").SemiBold();
                                table1.Cell().Row(2).Column(2).Background(Colors.Green.Lighten3).Padding(5)
                                    .Text(variedad.CalidadGrano?.nivel_calidad ?? "N/A");

                                if (variedad.InformacionAgronomica != null)
                                {
                                    table1.Cell().Row(3).Column(1).Background(Colors.Green.Lighten2).Padding(5)
                                        .Text("Tiempo de cosecha:").SemiBold();
                                    table1.Cell().Row(3).Column(2).Background(Colors.Green.Lighten2).Padding(5)
                                        .Text(variedad.InformacionAgronomica.tiempo_cosecha);

                                    table1.Cell().Row(4).Column(1).Background(Colors.Green.Lighten3).Padding(5)
                                        .Text("Maduración:").SemiBold();
                                    table1.Cell().Row(4).Column(2).Background(Colors.Green.Lighten3).Padding(5)
                                        .Text(variedad.InformacionAgronomica.maduracion);
                                }
                            });
                        });

                        // Tablas de detalles
                        variedadColumn.Item().PaddingTop(10).CornerRadius(10).Row(detailsRow =>
                        {
                            // Tabla Detalles grano
                            detailsRow.RelativeItem().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(2);
                                });

                                table.Cell().ColumnSpan(2).Background(Colors.Green.Darken1)
                                    .CornerRadiusTopLeft(10).Padding(5)
                                    .Text("Detalles grano").FontColor(Colors.White).SemiBold().AlignCenter();

                                table.Cell().Background(Colors.Green.Lighten2).Padding(5)
                                    .Text("Grupo Genético:").SemiBold().FontSize(10);
                                table.Cell().Background(Colors.Green.Lighten2).Padding(5)
                                    .Text(variedad.GrupoGenetico?.nombre_grupo ?? "N/A").FontSize(10);

                                table.Cell().Background(Colors.Green.Lighten3).Padding(5)
                                    .Text("Porte:").SemiBold().FontSize(10);
                                table.Cell().Background(Colors.Green.Lighten3).Padding(5)
                                    .Text(variedad.Porte?.nombre_porte ?? "N/A").FontSize(10);

                                table.Cell().Background(Colors.Green.Lighten2).Padding(5)
                                    .Text("Tamaño de grano:").SemiBold().FontSize(10);
                                table.Cell().Background(Colors.Green.Lighten2).Padding(5)
                                    .Text(variedad.TamanoGrano?.nombre_tamano ?? "N/A").FontSize(10);

                                table.Cell().Background(Colors.Green.Lighten3).Padding(5)
                                    .Text("Altitud óptima:").SemiBold().FontSize(10);
                                table.Cell().Background(Colors.Green.Lighten3).Padding(5)
                                    .CornerRadiusBottomLeft(10)
                                    .Text(variedad.AltitudOptima?.rango_altitud ?? "N/A").FontSize(10);
                            });

                            // Tabla Detalles agronómicos
                            detailsRow.RelativeItem().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(2);
                                });

                                table.Cell().ColumnSpan(2).Background(Colors.Green.Darken1)
                                    .CornerRadiusTopRight(10).Padding(5)
                                    .Text("Detalles agronómicos").FontColor(Colors.White).SemiBold().AlignCenter();

                                table.Cell().Background(Colors.Green.Lighten2).Padding(5)
                                    .Text("Potencial de rendimiento:").SemiBold().FontSize(10);
                                table.Cell().Background(Colors.Green.Lighten2).Padding(5)
                                    .Text(variedad.PotencialRendimiento?.nivel_rendimiento ?? "N/A").FontSize(10);

                                table.Cell().Background(Colors.Green.Lighten3).Padding(5)
                                    .Text("Calidad de grano:").SemiBold().FontSize(10);
                                table.Cell().Background(Colors.Green.Lighten3).Padding(5)
                                    .Text(variedad.CalidadGrano?.nivel_calidad ?? "N/A").FontSize(10);

                                table.Cell().Background(Colors.Green.Lighten2).Padding(5)
                                    .Text("Tiempo de cosecha:").SemiBold().FontSize(10);
                                table.Cell().Background(Colors.Green.Lighten2).Padding(5)
                                    .Text(variedad.InformacionAgronomica?.tiempo_cosecha ?? "N/A").FontSize(10);

                                table.Cell().Background(Colors.Green.Lighten3).Padding(5)
                                    .Text("Maduración:").SemiBold().FontSize(10);
                                table.Cell().Background(Colors.Green.Lighten3).Padding(5)
                                    .CornerRadiusBottomRight(10)
                                    .Text(variedad.InformacionAgronomica?.maduracion ?? "N/A").FontSize(10);
                            });
                        });
                    });

                    // Resistencias
                    variedadColumn.Item().PaddingTop(5).Text("Resistencias:").SemiBold();

                    if (variedad.VariedadesResistencia?.Any() == true)
                    {
                        foreach (var vr in variedad.VariedadesResistencia)
                        {
                            variedadColumn.Item().Text(
                                $"- {(vr.TipoResistencia?.nombre_tipo ?? "Tipo desconocido")}: " +
                                $"{(vr.NivelResistencia?.nombre_nivel ?? "Nivel desconocido")}");
                        }
                    }
                    else
                    {
                        variedadColumn.Item().Text("No hay resistencias registradas.");
                    }

                    // Descripción general
                    variedadColumn.Item().PaddingTop(5).Text("Descripción:").SemiBold();
                    variedadColumn.Item().Text(variedad.descripcion_general);
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
        public async Task<byte[]> Imgfromurl(Variedad variedad)
        {
            var imagenesVariedades = new Dictionary<int, byte[]>();
            if (!string.IsNullOrEmpty(variedad.imagen_referencia_url))
            {
                try
                {
                    var imageBytes = await LoadImageFromUrl(variedad.imagen_referencia_url);
                    imagenesVariedades[variedad.id] = imageBytes;
                    return imageBytes;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error cargando imagen para {variedad.nombre_comun}: {ex.Message}");
                    return null;
                }
            }
            return null;
        }
    }
}