using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Liga_futbol.Src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using Liga_Futbol.src.Shared.Helpers;
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
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .AlignCenter()
                        .Text("Catálogo de Variedades de Café Colombiano")
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(10);

                            foreach (var variedad in variedades)
                            {
                                // Sección por variedad
                                column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(variedadColumn =>
                                {
                                    variedadColumn.Item().Text($"{variedad.nombre_comun} ({variedad.nombre_cientifico})")
                                        .SemiBold().FontSize(16);

                                    variedadColumn.Item().PaddingTop(5).Row(row =>
                                    {
                                        row.RelativeItem().Column(detailsColumn =>
                                        {
                                            _ = detailsColumn.Item().Text($"Grupo Genético: {(variedad.GrupoGenetico != null ? variedad.GrupoGenetico.nombre_grupo : "N/A")}");
                                            _ = detailsColumn.Item().Text($"Porte: {variedad.Porte.nombre_porte}");
                                            detailsColumn.Item().Text($"Tamaño de grano: {variedad.TamanoGrano.nombre_tamano}");
                                            detailsColumn.Item().Text($"Altitud óptima: {variedad.AltitudOptima.rango_altitud}");
                                        });

                                        row.RelativeItem().Column(detailsColumn =>
                                        {
                                            detailsColumn.Item().Text($"Potencial de rendimiento: {variedad.PotencialRendimiento.nivel_rendimiento}");
                                            detailsColumn.Item().Text($"Calidad de grano: {variedad.CalidadGrano.nivel_calidad}");

                                            if (variedad.InformacionAgronomica != null)
                                            {
                                                detailsColumn.Item().Text($"Tiempo de cosecha: {variedad.InformacionAgronomica.tiempo_cosecha}");
                                                detailsColumn.Item().Text($"Maduración: {variedad.InformacionAgronomica.maduracion}");
                                            }
                                        });
                                    });

                                    // Resistencias
                                    variedadColumn.Item().PaddingTop(5).Text("Resistencias:").SemiBold();

                                    foreach (var vr in variedad.VariedadesResistencia)
                                    {
                                        variedadColumn.Item().Text($"- {vr.TipoResistencia.nombre_tipo}: {vr.NivelResistencia.nombre_nivel}");
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
