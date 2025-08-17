using Cafe_Colombiano.src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using Cafe_Colombiano.src.Shared.Helpers;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Cafe_Colombiano.src.Shared.Documentation.PdfTemplates
{
    public class CatalogTemplate : IDocument
    {
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
                        .FontSize(24)
                        .ExtraBold()
                        .Underline()
                        .AlignCenter();
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
        public void CompositionBody(IDocumentContainer container, DbContext context)
        {
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
    }
}