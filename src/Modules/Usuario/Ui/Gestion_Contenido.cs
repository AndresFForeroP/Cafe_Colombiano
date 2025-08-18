using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;

public class GestorVariedades
{
    private readonly VariedadRepository _repo;

    public GestorVariedades(VariedadRepository repo)
    {
        _repo = repo;
    }

    public GestorVariedades()
    {
    }

    public async Task<IEnumerable<Variedad>> BuscarYActualizarVariedadAsync()
    {
        var lista = await _repo.GetAllVariedadesAsync();

        if (lista == null || !lista.Any())
        {
            AnsiConsole.MarkupLine("[red]❌ No hay variedades en la base de datos[/]");
            return Enumerable.Empty<Variedad>();
        }

        // Selección de criterio de búsqueda
        var opcion = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]🔍 Buscar variedad por[/]")
                .PageSize(5)
                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                .AddChoices(new[]
                {
                    "1. ID",
                    "2. Nombre común",
                    "3. Nombre científico",
                    "9. Cancelar búsqueda"
                }));

        int criterio = opcion.StartsWith("1") ? 1 :
                       opcion.StartsWith("2") ? 2 :
                       opcion.StartsWith("3") ? 3 : 9;

        IEnumerable<Variedad> resultado = Enumerable.Empty<Variedad>();

        switch (criterio)
        {
            case 1:
                int idBuscado = AnsiConsole.Ask<int>("[green]Ingrese el ID de la variedad:[/]");
                resultado = lista.Where(v => v.id == idBuscado);
                break;

            case 2:
                string nombre = AnsiConsole.Ask<string>("[green]Ingrese el nombre común:[/]");
                resultado = lista.Where(v => !string.IsNullOrWhiteSpace(v.nombre_comun) &&
                                             v.nombre_comun.Contains(nombre, StringComparison.OrdinalIgnoreCase));
                break;

            case 3:
                string nombreCientifico = AnsiConsole.Ask<string>("[green]Ingrese el nombre científico:[/]");
                resultado = lista.Where(v => !string.IsNullOrWhiteSpace(v.nombre_cientifico) &&
                                             v.nombre_cientifico.Contains(nombreCientifico, StringComparison.OrdinalIgnoreCase));
                break;

            default:
                AnsiConsole.MarkupLine("[red]❌ Búsqueda cancelada[/]");
                return Enumerable.Empty<Variedad>();
        }

        if (!resultado.Any())
        {
            AnsiConsole.MarkupLine("[red]❌ No se encontraron resultados[/]");
            return Enumerable.Empty<Variedad>();
        }

        // Mostrar tabla inicial
        MostrarTabla(resultado);

        // Iterar sobre resultados para abrir y actualizar URLs
        foreach (var variedad in resultado)
        {
            if (!string.IsNullOrWhiteSpace(variedad.imagen_referencia_url))
            {
                AnsiConsole.MarkupLine($"[yellow]🌐 URL actual:[/] {variedad.imagen_referencia_url}");
                AbrirUrl(variedad.imagen_referencia_url);
                await Task.Delay(500);

                bool cambiarUrl = AnsiConsole.Confirm("¿Desea modificar esta URL?");
                if (cambiarUrl)
                {
                    string nuevaUrl = AnsiConsole.Ask<string>("[green]Ingrese la nueva URL:[/]");
                    variedad.imagen_referencia_url = nuevaUrl;
                    await _repo.UpdateVariedadAsync(variedad);
                    AnsiConsole.MarkupLine("[green]✅ URL actualizada y guardada en la base de datos[/]");
                }
            }
            else
            {
                bool agregarUrl = AnsiConsole.Confirm("No hay URL. ¿Desea agregar una?");
                if (agregarUrl)
                {
                    string nuevaUrl = AnsiConsole.Ask<string>("[green]Ingrese la URL:[/]");
                    variedad.imagen_referencia_url = nuevaUrl;
                    await _repo.UpdateVariedadAsync(variedad);
                    AnsiConsole.MarkupLine("[green]✅ URL agregada y guardada en la base de datos[/]");
                }
            }
        }

        // Mostrar tabla final con URLs actualizadas
        MostrarTabla(resultado);

        return resultado;
    }

    private void MostrarTabla(IEnumerable<Variedad> lista)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Green)
            .Title("[bold yellow]Resultados[/]");

        table.AddColumn("[cyan]ID[/]");
        table.AddColumn("[cyan]Nombre común[/]");
        table.AddColumn("[cyan]Nombre científico[/]");
        table.AddColumn("[cyan]URL[/]");

        foreach (var v in lista)
        {
            table.AddRow(
                v.id.ToString(),
                v.nombre_comun ?? "N/A",
                v.nombre_cientifico ?? "N/A",
                v.imagen_referencia_url ?? "N/A"
            );
        }

        AnsiConsole.Write(table);
    }

    private void AbrirUrl(string url)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error al abrir la URL:[/] {ex.Message}");
        }
    }

    internal async Task BuscarVariedadAsync(IEnumerable<Variedad> variedades)
    {
        throw new NotImplementedException();
    }
}
