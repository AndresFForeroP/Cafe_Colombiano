
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Variedad.Ui
{
    public class DIbujoMenusFiltrar : IMenusfiltros
    {
        public string MenuNombre()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>("[bold yellow]Ingrese el nombre científico del grano que desea buscar:[/]")
                    .PromptStyle("green")
                    .AllowEmpty()
            );
        }
        public string MenuPorte()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Porte[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Alto",
                        "2. Bajo",
                        "3. Dwarf/Compact",
                        "4. Tall",
                        "5. Desconocido",
                        "9. Cancelar Filtro"
                    }));
            return opcion.StartsWith("1") ? "Alto" :
                        opcion.StartsWith("2") ? "Bajo" :
                        opcion.StartsWith("3") ? "Dwarf/Compact" :
                        opcion.StartsWith("4") ? "Tall" :
                        opcion.StartsWith("5") ? "Desconocido" : "";
        }
        public string Menutamano()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Tamaño[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Pequeño",
                        "2. Mediano",
                        "3. Grande",
                        "4. Muy Grande",
                        "5. Desconocido",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "Pequeño" :
                        opcion.StartsWith("2") ? "Mediano" :
                        opcion.StartsWith("3") ? "Grande" :
                        opcion.StartsWith("4") ? "Muy Grande" :
                        opcion.StartsWith("5") ? "Desconocido" : "";
        }
        public string MenuAltitud()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Altitud[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. 500-1000 msnm",
                        "2. 400-900 msnm",
                        "3. 500-800 msnm",
                        "4. 700 msnm",
                        "5. 1200-1800 msnm",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "500-1000 msnm" :
                        opcion.StartsWith("2") ? "400-900 msnm" :
                        opcion.StartsWith("3") ? "500-800 msnm" :
                        opcion.StartsWith("4") ? "700 msnm" :
                        opcion.StartsWith("5") ? "1200-1800 msnm" : "";
        }
        public string MenuRendimiento()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Rendimiento[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Bajo (menos de 1500 kg/ha)",
                        "2. Medio (1500-3000 kg/ha)",
                        "3. Alto (3000-5000 kg/ha)",
                        "4. Muy alto (más de 5000 kg/ha)",
                        "5. Desconocido",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "Bajo (menos de 1500 kg/ha)" :
                            opcion.StartsWith("2") ? "Medio (1500-3000 kg/ha)" :
                            opcion.StartsWith("3") ? "Alto (3000-5000 kg/ha)" :
                            opcion.StartsWith("4") ? "Muy alto (más de 5000 kg/ha)" :
                            opcion.StartsWith("5") ? "Desconocido" : "";
        }
        public string MenuCalidad()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Calidad[/]")
                    .PageSize(8)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Excelente",
                        "2. Muy buena",
                        "3. Buena",
                        "4. Regular",
                        "5. Básica",
                        "6. Desconocida",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "Excelente" :
                        opcion.StartsWith("2") ? "Muy buena" :
                        opcion.StartsWith("3") ? "Buena" :
                        opcion.StartsWith("4") ? "Regular" :
                        opcion.StartsWith("5") ? "Básica" :
                        opcion.StartsWith("6") ? "Desconocida" : "";
        }
        public string Menuresistencias()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Resistencias[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Roya del cafeto",
                        "2. Enfermedad de la cereza del café (CBD)",
                        "3. Nematodos",
                        "4. Broca del café",
                        "5. Barrenador del tallo (Xylosandus compactus)",
                        "6. Marchitez del café (CWD)",
                        "7. Antracnosis",
                        "8. Enfermedad de la ampolla roja",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "Roya del cafeto" :
                    opcion.StartsWith("2") ? "Enfermedad de la cereza del café (CBD)" :
                    opcion.StartsWith("3") ? "Nematodos" :
                    opcion.StartsWith("4") ? "Broca del café" :
                    opcion.StartsWith("5") ? "Barrenador del tallo (Xylosandus compactus)" :
                    opcion.StartsWith("6") ? "Marchitez del café (CWD)" :
                    opcion.StartsWith("7") ? "Antracnosis" :
                    opcion.StartsWith("8") ? "Enfermedad de la ampolla roja" : "";
        }
        public string MenuNivelResistencias()
        {
            var opcionNivel = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Nivel de Resistencia[/]")
                    .PageSize(6)
                    .HighlightStyle(new Style(foreground: Color.Green, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Resistente",
                        "2. Tolerante",
                        "3. Susceptible",
                        "4. Desconocido",
                        "9. Cancelar Filtro"
                    }));

            return opcionNivel.StartsWith("1") ? "Resistente" :
                        opcionNivel.StartsWith("2") ? "Tolerante" :
                        opcionNivel.StartsWith("3") ? "Susceptible" :
                        opcionNivel.StartsWith("4") ? "Desconocido" : "";
        }
        public string MenuTiempoCosecha()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Tiempo de Cosecha[/]")
                    .PageSize(6)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. 6-8 meses",
                        "2. Año 2",
                        "3. Año 4",
                        "4. Desconocida",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "Principal: 6-8 meses después de floración" :
                        opcion.StartsWith("2") ? "Año 2" :
                        opcion.StartsWith("3") ? "Año 4" :
                        opcion.StartsWith("4") ? "Desconocida" : "";
        }
        public string MenuMaduracion()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Maduración[/]")
                    .PageSize(5)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Promedio",
                        "2. Tardía",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "Promedio" :
                            opcion.StartsWith("2") ? "Tardía" : "";
        }
        public string MenuNutricion()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Nutrición[/]")
                    .PageSize(5)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Media",
                        "2. Alta",
                        "3. Desconocida",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "Media" :
                            opcion.StartsWith("2") ? "Alta" :
                            opcion.StartsWith("3") ? "Desconocida" : "";
        }
        public string MenuDensidad()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Densidad de Siembra[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. 1000-2000 plantas/ha (usando poda de un solo tallo)",
                        "2. 2,500 árboles/ha",
                        "3. hasta 3,000 plantas/ha",
                        "4. 2000-3000 plantas/ha (usando poda de múltiples tallos)",
                        "5. hasta 10,000 cafetos/ha",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "1000-2000 plantas/ha (usando poda de un solo tallo)" :
                        opcion.StartsWith("2") ? "2,500 árboles/ha" :
                        opcion.StartsWith("3") ? "hasta 3,000 plantas/ha" :
                        opcion.StartsWith("4") ? "2000-3000 plantas/ha (usando poda de múltiples tallos)" :
                        opcion.StartsWith("5") ? "hasta 10,000 cafetos/ha" : "";
        }
        public string MenuGrupo()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Grupo Genético[/]")
                    .PageSize(8)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Arábigo",
                        "2. Guinea",
                        "3. Congo",
                        "4. Uganda",
                        "5. Guinea x Congo",
                        "6. Guinea x Coffea congensis",
                        "9. Cancelar Filtro"
                    }));

            return opcion.StartsWith("1") ? "Arábigo" :
                        opcion.StartsWith("2") ? "Guinea" :
                        opcion.StartsWith("3") ? "Congo" :
                        opcion.StartsWith("4") ? "Uganda" :
                        opcion.StartsWith("5") ? "Guinea x Congo" :
                        opcion.StartsWith("6") ? "Guinea x Coffea congensis" : "";
        }
        public int ImprimirMenuFiltros(List<string> filtros)
        {
            var opciones = new List<string>();

            if (!filtros.Contains("nombre")) opciones.Add("1. Filtrar por Nombre de grano");
            if (!filtros.Contains("porte")) opciones.Add("2. Filtrar por Porte del grano");
            if (!filtros.Contains("tamano")) opciones.Add("3. Filtrar por Tamaño del grano");
            if (!filtros.Contains("altitud")) opciones.Add("4. Filtrar por Altitud del grano");
            if (!filtros.Contains("rendimiento")) opciones.Add("5. Filtrar por Rendimiento del grano");
            if (!filtros.Contains("calidad")) opciones.Add("6. Filtrar por Calidad del grano");
            if (!filtros.Contains("resistencia")) opciones.Add("7. Filtrar por Resistencia del grano");
            if (!filtros.Contains("tiempocosecha")) opciones.Add("8. Filtrar por Tiempo de cosecha del grano");
            if (!filtros.Contains("maduracion")) opciones.Add("9. Filtrar por Maduración del grano");
            if (!filtros.Contains("nutricion")) opciones.Add("10. Filtrar por Nutrición del grano");
            if (!filtros.Contains("densidad")) opciones.Add("11. Filtrar por Densidad del grano");
            if (!filtros.Contains("grupo")) opciones.Add("12. Filtrar por Grupo del grano");

            if (opciones.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]Ya has aplicado todos los filtros[/]");
            }

            AnsiConsole.Write(new Rule("[yellow]FILTROS DISPONIBLES[/]").RuleStyle("green").Centered());

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Seleccione el filtro que desea aplicar[/]")
                    .PageSize(10)
                    .AddChoices(opciones)
            );
            AnsiConsole.MarkupLine($"[bold green]Seleccionaste:[/] {opcion}");
            return opcion.StartsWith("1") ? 1 :
                        opcion.StartsWith("2") ? 2 :
                        opcion.StartsWith("3") ? 3 :
                        opcion.StartsWith("4") ? 4 :
                        opcion.StartsWith("5") ? 5 :
                        opcion.StartsWith("6") ? 6 :
                        opcion.StartsWith("7") ? 7 :
                        opcion.StartsWith("8") ? 8 :
                        opcion.StartsWith("9") ? 9 :
                        opcion.StartsWith("10") ? 10 :
                        opcion.StartsWith("11") ? 11 :
                        opcion.StartsWith("12") ? 12 : 99;
        }
    }
}