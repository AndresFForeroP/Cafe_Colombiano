using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Liga_futbol.Src.Shared.Context;
using Microsoft.EntityFrameworkCore;
namespace Cafe_Colombiano.src.Shared.Helpers
{
    public class PdfAdministrator
    {

        public void GenerateSamplePdf(AppDbContext context)
        {
            context.Database.EnsureCreated();
            QuestPDF.Settings.License = LicenseType.Community;
           var variedades = context.Variedad
                .Include(v => v.GrupoGenetico)
                .Include(v => v.Porte)
                .Include(v => v.TamanoGrano)
                .Include(v => v.AltitudOptima)
                .Include(v => v.PotencialRendimiento)
                .Include(v => v.CalidadGrano)
                .Include(v => v.InformacionAgronomica)
                .Include(v => v.Resistencias)
                    .ThenInclude(r => r.TipoResistencia)
                .Include(v => v.Resistencias)
                    .ThenInclude(r => r.NivelResistencia)
                .ToList();

            Document.Create(container =>
            {
                container.Page(page =>
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
                                    variedadColumn.Item().Text($"{variedad.NombreComun} ({variedad.NombreCientifico})")
                                        .SemiBold().FontSize(16);
                                    
                                    variedadColumn.Item().PaddingTop(5).Row(row =>
                                    {
                                        row.RelativeItem().Column(detailsColumn =>
                                        {
                                            detailsColumn.Item().Text($"Grupo Genético: {variedad.GrupoGenetico.NombreGrupo}");
                                            detailsColumn.Item().Text($"Porte: {variedad.Porte.NombrePorte}");
                                            detailsColumn.Item().Text($"Tamaño de grano: {variedad.TamanoGrano.NombreTamano}");
                                            detailsColumn.Item().Text($"Altitud óptima: {variedad.AltitudOptima.RangoAltitud}");
                                        });
                                        
                                        row.RelativeItem().Column(detailsColumn =>
                                        {
                                            detailsColumn.Item().Text($"Potencial de rendimiento: {variedad.PotencialRendimiento.NivelRendimiento}");
                                            detailsColumn.Item().Text($"Calidad de grano: {variedad.CalidadGrano.NivelCalidad}");
                                            
                                            if (variedad.InformacionAgronomica != null)
                                            {
                                                detailsColumn.Item().Text($"Tiempo de cosecha: {variedad.InformacionAgronomica.TiempoCosecha}");
                                                detailsColumn.Item().Text($"Maduración: {variedad.InformacionAgronomica.Maduracion}");
                                            }
                                        });
                                    });
                                    
                                    // Resistencias
                                    if (variedad.Resistencias.Any())
                                    {
                                        variedadColumn.Item().PaddingTop(5).Text("Resistencias:").SemiBold();
                                        
                                        foreach (var resistencia in variedad.Resistencias)
                                        {
                                            variedadColumn.Item().Text($"- {resistencia.TipoResistencia.NombreTipo}: {resistencia.NivelResistencia.NombreNivel}");
                                        }
                                    }
                                    
                                    // Descripción general
                                    if (!string.IsNullOrEmpty(variedad.DescripcionGeneral))
                                    {
                                        variedadColumn.Item().PaddingTop(5).Text("Descripción:").SemiBold();
                                        variedadColumn.Item().Text(variedad.DescripcionGeneral);
                                    }
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
