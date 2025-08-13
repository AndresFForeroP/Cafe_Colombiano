using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Cafe_Colombiano.src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Shared.Helpers;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using System.Threading.Tasks;
//en proceso, aun no crea el pdf y hay problemas leyendo el contexto de la base de datos
namespace Cafe_Colombiano.src.Shared.Helpers
{
    public class PdfAdministrator
    {

        public async Task GenerateSamplePdf(AppDbContext context)
        {
            context.Database.EnsureCreated();
            QuestPDF.Settings.License = LicenseType.Community;
            var contexto = DbContextFactory.Create();
            var repo = new VariedadRepository(contexto);
            var variedades = await repo.GetAllVariedadesAsync();



            Document.Create(container =>
            {
                _ = container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    page.DefaultTextStyle(x => x.FontFamily("verdana"));
                    page.Header()
                        .AlignCenter()
                        .Text("Catálogo de Variedades de Café Colombiano")
                        .ExtraBold().FontSize(24).FontColor(Colors.Green.Darken2);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(10);

                            foreach (var variedad in variedades)
                            {
                                // Sección por variedad
                                column.Item().Background(Colors.Green.Lighten3).CornerRadius(10).Border(1).BorderColor(Colors.Green.Darken2).Padding(15).Column(variedadColumn =>
                                {
                                    variedadColumn.Item().Text($"{variedad.nombre_comun} ({variedad.nombre_cientifico})").ExtraBold().FontSize(16).FontColor(Colors.Green.Darken4).AlignRight();

                                    variedadColumn.Item().PaddingTop(10).Row(row =>
                                    {
                                        //para la imagen
                                        // row.AutoItem().Image(variedad.ImagenUrl).Height(100).Width(100).Alignment(QuestPDF.Infrastructure.ImageAlignment.Left);
                                        
                                        variedadColumn.Item().PaddingTop(10).Row(row =>
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

                                                table.Cell().Text("Grupo Genético:").SemiBold().BackgroundColor(Colors.Green.Lighten2).FontSize(10);
                                                table.Cell().Text(variedad.GrupoGenetico?.nombre_grupo ?? "N/A").BackgroundColor(Colors.Green.Lighten2).FontSize(10);

                                                table.Cell().Text("Porte:").SemiBold().BackgroundColor(Colors.Green.Lighten3).FontSize(10);
                                                table.Cell().Text(variedad.Porte?.nombre_porte ?? "N/A").BackgroundColor(Colors.Green.Lighten3).FontSize(10);

                                                table.Cell().Text("Tamaño de grano:").SemiBold().BackgroundColor(Colors.Green.Lighten2).FontSize(10);
                                                table.Cell().Text(variedad.TamanoGrano?.nombre_tamano ?? "N/A").BackgroundColor(Colors.Green.Lighten2).FontSize(10);

                                                table.Cell().Text("Altitud óptima:").SemiBold().BackgroundColor(Colors.Green.Lighten3).FontSize(10);
                                                table.Cell().Text(variedad.AltitudOptima?.rango_altitud ?? "N/A").BackgroundColor(Colors.Green.Lighten3).FontSize(10);
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

                                                table.Cell().Text("Potencial de rendimiento:").SemiBold().BackgroundColor(Colors.Green.Lighten2).FontSize(10);
                                                table.Cell().Text(variedad.PotencialRendimiento?.nivel_rendimiento ?? "N/A").BackgroundColor(Colors.Green.Lighten2).FontSize(10);

                                                table.Cell().Text("Calidad de grano:").SemiBold().BackgroundColor(Colors.Green.Lighten3).FontSize(10);
                                                table.Cell().Text(variedad.CalidadGrano?.nivel_calidad ?? "N/A").BackgroundColor(Colors.Green.Lighten3).FontSize(10);

                                                table.Cell().Text("Tiempo de cosecha:").SemiBold().BackgroundColor(Colors.Green.Lighten2).FontSize(10);
                                                table.Cell().Text(variedad.InformacionAgronomica?.tiempo_cosecha ?? "N/A").BackgroundColor(Colors.Green.Lighten2).FontSize(10);

                                                table.Cell().Text("Maduración:").SemiBold().BackgroundColor(Colors.Green.Lighten3).FontSize(10);
                                                table.Cell().Text(variedad.InformacionAgronomica?.maduracion ?? "N/A").BackgroundColor(Colors.Green.Lighten3).FontSize(10);
                                            });
                                        });
                                        row.RelativeItem().Column(detailsColumn =>
                                        {
                                            detailsColumn.Item().Text($"Potencial de rendimiento: {(variedad.PotencialRendimiento != null ? variedad.PotencialRendimiento.nivel_rendimiento : "N/A")}");
                                            detailsColumn.Item().Text($"Calidad de grano: {(variedad.CalidadGrano != null ? variedad.CalidadGrano.nivel_calidad : "N/A")}");

                                            if (variedad.InformacionAgronomica != null)
                                            {
                                                detailsColumn.Item().Text($"Tiempo de cosecha: {variedad.InformacionAgronomica.tiempo_cosecha}");
                                                detailsColumn.Item().Text($"Maduración: {variedad.InformacionAgronomica.maduracion}");
                                            }
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
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Página ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf("catalogo_cafe_colombiano.pdf");
        }
    }
}

// code in your main method
