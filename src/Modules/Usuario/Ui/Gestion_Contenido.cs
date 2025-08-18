 using Spectre.Console;
 using System;
 using System.Collections.Generic;
 using System.Diagnostics;
 using System.Linq;
 using System.Threading.Tasks;
 using Cafe_Colombiano.src.Modules.Variedad.Domain.Entities;
 
 
 public class GestorVariedades
 {
     public async Task<IEnumerable<Variedad>> BuscarVariedadAsync(IEnumerable<Variedad> Lista)
     {
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
                 resultado = Lista.Where(v => v.id == idBuscado);
                 break;
 
             case 2:
                 string nombre = AnsiConsole.Ask<string>("[green]Ingrese el nombre común:[/]");
                 resultado = Lista.Where(v => v.imagen_referencia_url != null &&
                                              v.imagen_referencia_url.Contains(nombre, StringComparison.OrdinalIgnoreCase));
                 break;
 
             case 3:
                 string nombreCientifico = AnsiConsole.Ask<string>("[green]Ingrese el nombre científico:[/]");
                 resultado = Lista.Where(v => v.nombre_cientifico != null &&
                                              v.nombre_cientifico.Contains(nombreCientifico, StringComparison.OrdinalIgnoreCase));
                 break;
 
             default:
                 AnsiConsole.MarkupLine("[red]❌ Búsqueda cancelada[/]");
                 return Enumerable.Empty<Variedad>();
         }
 
         if (resultado.Any())
         {
             // Crear tabla
             var table = new Table()
                 .Border(TableBorder.Rounded)
                 .BorderColor(Color.Green)
                 .Title("[bold yellow]Resultados de la búsqueda[/]");
 
             table.AddColumn("[cyan]ID[/]");
             table.AddColumn("[cyan]Nombre común[/]");
             table.AddColumn("[cyan]Nombre científico[/]");
             table.AddColumn("[cyan]URL[/]");
 
             foreach (var variedad in resultado)
             {
                 table.AddRow(
                     variedad.id.ToString(),
                     variedad.nombre_comun?? "N/A",
                     variedad.nombre_cientifico ?? "N/A",
                     variedad.imagen_referencia_url ?? "N/A"
                 );
 
                 if (!string.IsNullOrWhiteSpace(variedad.imagen_referencia_url))
                 {
                     AnsiConsole.MarkupLine($"[yellow]🌐 Abriendo URL:[/] {variedad.imagen_referencia_url}");
                     AbrirUrl(variedad.imagen_referencia_url);
                     await Task.Delay(500); // pausa breve entre aperturas
                 }
             }
 
             AnsiConsole.Write(table);
         }
         else
         {
             AnsiConsole.MarkupLine("[red]❌ No se encontraron resultados[/]");
         }
 
         return resultado;
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
 }
 