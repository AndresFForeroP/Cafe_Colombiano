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
            container.Page(page =>
            {
                page.Content().Column(column =>
                {
                    column.Item().Text("Catálogo de Variedades de Café Colombiano");
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