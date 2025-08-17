using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Cafe_Colombiano.src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Shared.Helpers;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using System.Threading.Tasks;
//en proceso, aun no crea el pdf y hay problemas leyendo el contexto de la base de datos
namespace Cafe_Colombiano.src.Shared.Documentation
{
    public class PdfAdministrator
    {

        public static async Task GenerateSamplePdf(AppDbContext context)
        {
            context.Database.EnsureCreated();
            QuestPDF.Settings.License = LicenseType.Community;
            var contexto = DbContextFactory.Create();
            var repo = new VariedadRepository(contexto);
            var variedades = await repo.GetAllVariedadesAsync();
            await Document.Create(async container =>
            {
                _ = container.Page(async page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    page.DefaultTextStyle(x => x.FontFamily("verdana"));
                    page.Header()
                        .AlignCenter()
                        .Text("Catálogo de Variedades de Café Colombiano")
                        .ExtraBold()
                        .FontSize(24)
                        .FontColor(Colors.Green.Darken2);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(async column =>
                        {
                            column.Spacing(10);

                            foreach (var variedad in variedades)
                            {
                                var imageBytes = await LoadImageFromUrl(variedad.imagen_referencia_url);
                                // Sección por variedad
                                column.Item().Background(Colors.Green.Lighten5).CornerRadius(10).Border(1).BorderColor(Colors.Green.Darken2).Padding(20).Column(variedadColumn =>
                                {
                                    variedadColumn.Item().Row(row =>
                                    {
                                        // Buscar imagen que coincida con el nombre
                                        row.RelativeItem().Height(100).Width(100).Image(variedad.imagen_referencia_url != null ? variedad.imagen_referencia_url ).AspectRatio(1).AlignCenter().Padding(5).Border(1).BorderColor(Colors.Green.Darken2).CornerRadius(10);
                                        row.RelativeItem().Text($"{variedad.nombre_comun} ({variedad.nombre_cientifico})").ExtraBold().FontSize(20).FontColor(Colors.Green.Darken4).AlignRight();
                                    });
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
                                                table1.Cell().Row(1).Column(1).Background(Colors.Green.Lighten2).Padding(5).Text($"Potencial de rendimiento:").SemiBold();
                                                table1.Cell().Row(1).Column(2).Background(Colors.Green.Lighten2).Padding(5).Text($"{(variedad.PotencialRendimiento != null ? variedad.PotencialRendimiento.nivel_rendimiento : "N/A")}");
                                                table1.Cell().Row(2).Column(1).Background(Colors.Green.Lighten3).Padding(5).Text($"Calidad de grano: ").SemiBold();
                                                table1.Cell().Row(2).Column(2).Background(Colors.Green.Lighten3).Padding(5).Text($"{(variedad.CalidadGrano != null ? variedad.CalidadGrano.nivel_calidad : "N/A")}");
                                                if (variedad.InformacionAgronomica != null)
                                                {
                                                    table1.Cell().Row(3).Column(1).Background(Colors.Green.Lighten2).Padding(5).Text($"Tiempo de cosecha:").SemiBold();
                                                    table1.Cell().Row(3).Column(2).Background(Colors.Green.Lighten2).Padding(5).Text($"{variedad.InformacionAgronomica.tiempo_cosecha}");
                                                    table1.Cell().Row(4).Column(1).Background(Colors.Green.Lighten3).Padding(5).Text($"Maduración: ").SemiBold();
                                                    table1.Cell().Row(4).Column(2).Background(Colors.Green.Lighten3).Padding(5).Text($"{variedad.InformacionAgronomica.maduracion}");
                                                }
                                            });
                                        });
                                        variedadColumn.Item().PaddingTop(10).CornerRadius(10).Row(row =>
                                        {
                                            // Tabla Detalles grano
                                            row.RelativeItem().Table(table =>
                                            {
                                                table.ColumnsDefinition(columns =>
                                                {
                                                    columns.RelativeColumn(1);
                                                    columns.RelativeColumn(2);
                                                });

                                                // Encabezado
                                                //mover el background antes del texto e investigar como colocar las imagenes de fondo
                                                table.Cell().ColumnSpan(2).Background(Colors.Green.Darken1).CornerRadiusTopLeft(10).Padding(5)
                                                    .Text("Detalles grano").FontColor(Colors.White).SemiBold().AlignCenter();

                                                table.Cell().Background(Colors.Green.Lighten2).Padding(5).Text("Grupo Genético:").SemiBold().FontSize(10);
                                                table.Cell().Background(Colors.Green.Lighten2).Padding(5).Text(variedad.GrupoGenetico?.nombre_grupo ?? "N/A").FontSize(10);

                                                table.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Porte:").SemiBold().FontSize(10);
                                                table.Cell().Background(Colors.Green.Lighten3).Padding(5).Text(variedad.Porte?.nombre_porte ?? "N/A").FontSize(10);

                                                table.Cell().Background(Colors.Green.Lighten2).Padding(5).Text("Tamaño de grano:").SemiBold().FontSize(10);
                                                table.Cell().Background(Colors.Green.Lighten2).Padding(5).Text(variedad.TamanoGrano?.nombre_tamano ?? "N/A").FontSize(10);

                                                table.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Altitud óptima:").SemiBold().FontSize(10);
                                                table.Cell().Background(Colors.Green.Lighten3).Padding(5).CornerRadiusBottomLeft(10).Text(variedad.AltitudOptima?.rango_altitud ?? "N/A").FontSize(10);
                                            });

                                            // Tabla Detalles agronómicos
                                            row.RelativeItem().Table(table =>
                                            {
                                                table.ColumnsDefinition(columns =>
                                                {
                                                    columns.RelativeColumn(1);
                                                    columns.RelativeColumn(2);
                                                });

                                                // Encabezado
                                                table.Cell().ColumnSpan(2).Background(Colors.Green.Darken1).CornerRadiusTopRight(10).Padding(5)
                                                    .Text("Detalles agronómicos").FontColor(Colors.White).SemiBold().AlignCenter();

                                                table.Cell().Background(Colors.Green.Lighten2).Padding(5).Text("Potencial de rendimiento:").SemiBold().FontSize(10);
                                                table.Cell().Background(Colors.Green.Lighten2).Padding(5).Text(variedad.PotencialRendimiento?.nivel_rendimiento ?? "N/A").FontSize(10);

                                                table.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Calidad de grano:").SemiBold().FontSize(10);
                                                table.Cell().Background(Colors.Green.Lighten3).Padding(5).Text(variedad.CalidadGrano?.nivel_calidad ?? "N/A").FontSize(10);

                                                table.Cell().Background(Colors.Green.Lighten2).Padding(5).Text("Tiempo de cosecha:").SemiBold().FontSize(10);
                                                table.Cell().Background(Colors.Green.Lighten2).Padding(5).Text(variedad.InformacionAgronomica?.tiempo_cosecha ?? "N/A").FontSize(10);

                                                table.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Maduración:").SemiBold().FontSize(10);
                                                table.Cell().Background(Colors.Green.Lighten3).Padding(5).CornerRadiusBottomRight(10).Text(variedad.InformacionAgronomica?.maduracion ?? "N/A").FontSize(10);
                                            });
                                        });
                                    });

                                    // Resistencias
                                    variedadColumn.Item().PaddingTop(5).Text("Resistencias:").SemiBold();

                                    if (variedad.VariedadesResistencia != null)
                                    {
                                        foreach (var vr in variedad.VariedadesResistencia)
                                        {
                                            _ = variedadColumn.Item().Text($"- {(vr.TipoResistencia != null ? vr.TipoResistencia.nombre_tipo : "Tipo desconocido")}: {(vr.NivelResistencia != null ? vr.NivelResistencia.nombre_nivel : "Nivel desconocido")}");
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

                                column.Item().PaddingBottom(10);
                            }
                        });

                    page.Footer()
                        .AlignRight()
                        .Text(x =>
                        {
                            x.Span("Pg: ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf("catalogo_cafe_colombiano.pdf");
        }
        private static async Task<byte[]> LoadImageFromUrl(string url)
        {
            using var httpClient = new HttpClient();
            return await httpClient.GetByteArrayAsync(url);
        }
    }
}

// code in your main method
