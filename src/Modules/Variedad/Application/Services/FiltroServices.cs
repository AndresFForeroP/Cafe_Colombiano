using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class FiltroServices
    {
        private readonly IVariedadRepository _repo;
        public FiltroServices(IVariedadRepository _repo)
        {
            this._repo = _repo;
        }
        public async Task Filtrar()
        {
            List<string> FiltrosAplicados= [];
            Console.Clear();
            string Respuesta = "";
            int opmenufiltro = 0;
            var variedades = await _repo.GetAllVariedadesAsync();
            var Variedad = variedades;
            do
            {
                ImprimirMenuFiltros(FiltrosAplicados);
                opmenufiltro = validarentero();
                switch (opmenufiltro)
                {
                    case 1:
                        FiltrosAplicados.Add("nombre");
                        Variedad = _repo.FiltrarPorNombre(Variedad);
                        break;
                    case 2:
                        FiltrosAplicados.Add("porte");
                        Variedad = _repo.FiltrarPorPorte(Variedad);
                        break;
                    case 3:
                        FiltrosAplicados.Add("tamano");
                        Variedad = _repo.FiltrarPorTamano(Variedad);
                        break;
                    case 4:
                        FiltrosAplicados.Add("altitud");
                        Variedad = _repo.FiltrarPorAltitud(Variedad);
                        break;
                    case 5:
                        FiltrosAplicados.Add("rendimiento");
                        Variedad = _repo.FiltrarPorRendimiento(Variedad);
                        break;
                    case 6:
                        FiltrosAplicados.Add("calidad");
                        Variedad = _repo.FiltrarPorCalidad(Variedad);
                        break;
                    case 7:
                        FiltrosAplicados.Add("resistencia");
                        Variedad = _repo.FiltrarPorResistencia(Variedad);
                        break;
                    case 8:
                        FiltrosAplicados.Add("tiempocosecha");
                        Variedad = _repo.FiltrarPorTiempoCoshecha(Variedad);
                        break;
                    case 9:
                        FiltrosAplicados.Add("maduracion");
                        Variedad = _repo.FiltrarPorMaduracion(Variedad);
                        break;
                    case 10:
                        FiltrosAplicados.Add("nutricion");
                        Variedad = _repo.FiltrarPorNutricion(Variedad);
                        break;
                    case 11:
                        FiltrosAplicados.Add("densidad");
                        Variedad = _repo.FiltrarPorDensidad(Variedad);
                        break;
                    case 12:
                        FiltrosAplicados.Add("grupo");
                        Variedad = _repo.FiltrarPorGrupo(Variedad);
                        break;
                }
                Respuesta = Validarfiltros();
                Console.Clear();
            } while (Respuesta != "no");
            MostrarVariedades(Variedad);
        }
        private string Validarfiltros()
        {
            Console.Clear();
            Console.WriteLine("Desea agregar otro filtro a la busqueda?");
            string Respuesta = Console.ReadLine() ?? "";
            while (Respuesta.ToLower() != "no" && Respuesta.ToLower() != "si")
            {
                Console.WriteLine("Valor invalido");
                Console.WriteLine("Ingrese si para actualizar el torneo o no si desea volver a ingresar los datos");
                Respuesta = Console.ReadLine() ?? "";
            }
            return Respuesta.ToLower();
        }
        private int validarentero()
        {
            int id = 0;
            while (!int.TryParse(Console.ReadLine(), out id) || id <= 0 || id > 12)
            {
                Console.WriteLine("No es un opcion valida del menu");
            }
            return id;
        }
       private void ImprimirMenuFiltros(List<string> filtros)
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
                return;
            }

            AnsiConsole.Write(new Rule("[yellow]FILTROS DISPONIBLES[/]").RuleStyle("green").Centered());

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Seleccione el filtro que desea aplicar[/]")
                    .PageSize(10)
                    .AddChoices(opciones)
            );

            AnsiConsole.MarkupLine($"[bold green]Seleccionaste:[/] {opcion}");
        }


        private void MostrarVariedades(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> variedad)
        {
            if (variedad.Any())
            {
                foreach (var v in variedad)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"--- Variedad ---");
                    Console.WriteLine($"ID: {v.id}");
                    Console.WriteLine($"Nombre: {v.nombre_cientifico}");
                    Console.WriteLine($"Altitud Óptima: {v.AltitudOptima?.rango_altitud} - {v.AltitudOptima?.descripcion}");
                    Console.WriteLine($"Grupo Genético: {v.GrupoGenetico?.nombre_grupo} - {v.GrupoGenetico?.origen}");
                    Console.WriteLine($"Porte: {v.Porte?.nombre_porte}");
                    Console.WriteLine($"Tamaño Grano: {v.TamanoGrano?.nombre_tamano}");
                    Console.WriteLine($"Potencial Rendimiento: {v.PotencialRendimiento?.nivel_rendimiento}");
                    Console.WriteLine($"Calidad Grano: {v.CalidadGrano?.nivel_calidad}");
                    Console.WriteLine($"Información Agronómica:");
                    Console.WriteLine($"Maduracion {v.InformacionAgronomica?.maduracion}");
                    Console.WriteLine($"Tiempo de cosecha {v.InformacionAgronomica?.tiempo_cosecha}");
                    Console.WriteLine($"Nutricion {v.InformacionAgronomica?.nutricion}");
                    Console.WriteLine($"Densidad de siembra {v.InformacionAgronomica?.densidad_siembra}");

                    if (v.VariedadesResistencia != null && v.VariedadesResistencia.Any())
                    {
                        Console.WriteLine($"--- Resistencias ---");
                        foreach (var vr in v.VariedadesResistencia)
                        {
                            Console.WriteLine($"Nivel: {vr.NivelResistencia?.nombre_nivel}");
                            Console.WriteLine($"Tipo: {vr.TipoResistencia?.nombre_tipo}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No tiene resistencias registradas.");
                    }

                    Console.WriteLine(new string('-', 50)); // Separador
                }
            }
            else
            {
                Console.WriteLine("No existe variedad de cafe que cumpla con los filtros");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            
        } 
        
    }
}