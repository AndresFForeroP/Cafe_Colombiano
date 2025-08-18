using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Ui;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Usuario.Application.Services
{
    public class GestionServices
    {
        private readonly VariedadRepository _repo;

    // Constructor que recibe un repositorio (inyección de dependencias)
    public GestionServices(VariedadRepository repo)
    {
        _repo = repo;
    }
    public async Task<IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>> BuscarYActualizarVariedadAsync()
    {
        // Obtener todas las variedades de la base de datos
        var lista = await _repo.GetAllVariedadesAsync();

        if (lista == null || !lista.Any())
        {
            AnsiConsole.MarkupLine("[red]❌ No hay variedades en la base de datos[/]");
            return Enumerable.Empty<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>();
        }
        // Selección del criterio de búsqueda mediante un prompt interactivo
        var opcion = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]🔍 Buscar variedad por[/]") // Título del prompt
                .PageSize(5) // Número de opciones visibles
                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold)) // Estilo de la opción seleccionada
                .AddChoices(new[]
                {
                    "1. ID",
                    "2. Nombre común",
                    "3. Nombre científico",
                    "9. Cancelar búsqueda"
                }));
        // Determinar criterio según la opción seleccionada
        int criterio = opcion.StartsWith("1") ? 1 :
                       opcion.StartsWith("2") ? 2 :
                       opcion.StartsWith("3") ? 3 : 9;

        IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> resultado = Enumerable.Empty<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>();

        // Filtrado de la lista según el criterio
        switch (criterio)
        {
            case 1:
                // Búsqueda por ID
                int idBuscado = AnsiConsole.Ask<int>("[green]Ingrese el ID de la variedad:[/]");
                resultado = lista.Where(v => v.id == idBuscado);
                break;

            case 2:
                // Búsqueda por nombre común
                string nombre = AnsiConsole.Ask<string>("[green]Ingrese el nombre común:[/]");
                resultado = lista.Where(v => !string.IsNullOrWhiteSpace(v.nombre_comun) &&
                                             v.nombre_comun.Contains(nombre, StringComparison.OrdinalIgnoreCase));
                break;

            case 3:
                // Búsqueda por nombre científico
                string nombreCientifico = AnsiConsole.Ask<string>("[green]Ingrese el nombre científico:[/]");
                resultado = lista.Where(v => !string.IsNullOrWhiteSpace(v.nombre_cientifico) &&
                                             v.nombre_cientifico.Contains(nombreCientifico, StringComparison.OrdinalIgnoreCase));
                break;

            default:
                // Si se selecciona cancelar
                AnsiConsole.MarkupLine("[red]❌ Búsqueda cancelada[/]");
                return Enumerable.Empty<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>();
        }

        if (!resultado.Any())
        {
            AnsiConsole.MarkupLine("[red]❌ No se encontraron resultados[/]");
            return Enumerable.Empty<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>();
        }

        // Mostrar tabla inicial con los resultados encontrados
        MostrarTabla(resultado);

        // Iterar sobre cada variedad para abrir URL y permitir modificaciones
        foreach (var variedad in resultado)
        {
                if (!string.IsNullOrWhiteSpace(variedad.imagen_referencia_url))
                {
                    // Mostrar URL actual
                    AnsiConsole.MarkupLine($"[yellow]🌐 URL actual:[/] {variedad.imagen_referencia_url}");

                    // Abrir URL en el navegador
                    AbrirUrl(variedad.imagen_referencia_url);
                    await Task.Delay(500); // Pausa breve para la interacción visual

                    // Preguntar si desea modificar la URL
                    bool cambiarUrl = AnsiConsole.Confirm("¿Desea modificar esta URL?");
                    if (cambiarUrl)
                    {
                        string nuevaUrl = AnsiConsole.Ask<string>("[green]Ingrese la nueva URL:[/]");
                        variedad.imagen_referencia_url = nuevaUrl;

                        // Guardar cambios en la base de datos
                        //aqui es donde debes mirar el cambio con el guardado para poder hacer la modificacion en la base de datos
                        await _repo.SaveAsync();
                        AnsiConsole.MarkupLine("[green]✅ URL actualizada y guardada en la base de datos[/]");
                    }
                    var MenuPanelAdministrativo = new MenuPanelAdministrativo();
                    await MenuPanelAdministrativo.iniciar();
                
            }
                else
                {
                    // Si no hay URL, preguntar si desea agregar una
                    bool agregarUrl = AnsiConsole.Confirm("No hay URL. ¿Desea agregar una?");
                    if (agregarUrl)
                    {
                        string nuevaUrl = AnsiConsole.Ask<string>("[green]Ingrese la URL:[/]");
                        variedad.imagen_referencia_url = nuevaUrl;

                        // Guardar nueva URL en la base de datos
                        await _repo.SaveAsync();
                        AnsiConsole.MarkupLine("[green]✅ URL agregada y guardada en la base de datos[/]");
                    }
                }
        }

        // Mostrar tabla final con URLs actualizadas
        MostrarTabla(resultado);
        return resultado;
    }

    
    /// Muestra una tabla en consola con las variedades proporcionadas.
  
    private void MostrarTabla(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> lista)
    {
        var table = new Table()
            .Border(TableBorder.Rounded) // Borde redondeado
            .BorderColor(Color.Green) // Color del borde
            .Title("[bold yellow]Resultados[/]"); // Título de la tabla

        // Columnas de la tabla
        table.AddColumn("[cyan]ID[/]");
        table.AddColumn("[cyan]Nombre común[/]");
        table.AddColumn("[cyan]Nombre científico[/]");
        table.AddColumn("[cyan]URL[/]");

        // Agregar filas con los datos de cada variedad
        foreach (var v in lista)
        {
            table.AddRow(
                v.id.ToString(),
                v.nombre_comun ?? "N/A",
                v.nombre_cientifico ?? "N/A",
                v.imagen_referencia_url ?? "N/A"
            );
        }
        // Renderizar tabla en consola
        AnsiConsole.Write(table);
    }
    /// Abre una URL en el navegador predeterminado.
    private void AbrirUrl(string url)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true // Permite abrir la URL con la app predeterminada del sistema
            };
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error al abrir la URL:[/] {ex.Message}");
        }
    }
    }
}