using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
    {
    public class ActualizarVariedadService
    {
        private readonly IVariedadRepository _VariedadRepository;

        public ActualizarVariedadService(IVariedadRepository variedadRepository)
        {
            _VariedadRepository = variedadRepository;
        }

        public async Task ActualizarVariedad()
        {
            Console.Clear();
            Console.WriteLine("Actualizar Variedad");
            Console.WriteLine("-------------------");

            _VariedadRepository.MostrasListaIds();

            Console.Write("Ingrese el ID de la variedad a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var variedadToUpdate = await _VariedadRepository.GetVariedadByIdAsync(id);
                if (variedadToUpdate != null)
                {
                    // Datos principales
                    variedadToUpdate.nombre_comun = PedirDatoOpcional("Ingrese el nuevo nombre común de la variedad:", variedadToUpdate.nombre_comun);
                    variedadToUpdate.nombre_cientifico = PedirDatoOpcional("Ingrese el nuevo nombre científico de la variedad:", variedadToUpdate.nombre_cientifico);
                    variedadToUpdate.descripcion_general = PedirDatoOpcional("Ingrese la nueva descripción de la variedad:", variedadToUpdate.descripcion_general);
                    variedadToUpdate.historia_linaje = PedirDatoOpcional("Ingrese la nueva historia del linaje de la variedad:", variedadToUpdate.historia_linaje);
                    variedadToUpdate.imagen_referencia_url = PedirDatoOpcional("Ingrese la nueva URL de la imagen de referencia de la variedad:", variedadToUpdate.imagen_referencia_url);

                    // Grupo Genético
                    if (variedadToUpdate.GrupoGenetico != null)
                    {
                        var grupoOpcion = variedadToUpdate.GrupoGenetico.nombre_grupo;
                        var grupoMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de grupo Genético (actual: " + grupoOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. Arábigo",
                                    "2. Guinea",
                                    "3. Congo",
                                    "4. Uganda",
                                    "5. Guinea x Congo",
                                    "6. Guinea x Coffea congensis",
                                    "9. Mantener valor actual"
                                }));

                        if (!grupoMenu.StartsWith("9"))
                        {
                            int grupo = grupoMenu.StartsWith("1") ? 1 :
                                        grupoMenu.StartsWith("2") ? 2 :
                                        grupoMenu.StartsWith("3") ? 3 :
                                        grupoMenu.StartsWith("4") ? 4 :
                                        grupoMenu.StartsWith("5") ? 5 :
                                        grupoMenu.StartsWith("6") ? 6 : 0;

                            switch (grupo)
                            {
                                case 1: grupoOpcion = "Arabigo"; break;
                                case 2: grupoOpcion = "Guinea"; break;
                                case 3: grupoOpcion = "Congo"; break;
                                case 4: grupoOpcion = "Uganda"; break;
                                case 5: grupoOpcion = "Guinea x Congo"; break;
                                case 6: grupoOpcion = "Guinea x Coffea congensis"; break;
                            }
                            variedadToUpdate.GrupoGenetico.nombre_grupo = grupoOpcion;
                        }
                        // Origen sigue siendo texto libre
                        variedadToUpdate.GrupoGenetico.origen = PedirDatoOpcional("Ingrese el nuevo origen de la variedad:", variedadToUpdate.GrupoGenetico.origen);
                    }

                    // Porte
                    if (variedadToUpdate.Porte != null)
                    {
                        var porteOpcion = variedadToUpdate.Porte.nombre_porte;
                        var porteMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de Porte (actual: " + porteOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. Alto",
                                    "2. Bajo",
                                    "3. Dwarf/Compact",
                                    "4. Tall",
                                    "5. Desconocido",
                                    "9. Mantener valor actual"
                                }));

                        if (!porteMenu.StartsWith("9"))
                        {
                            int porte = porteMenu.StartsWith("1") ? 1 :
                                        porteMenu.StartsWith("2") ? 2 :
                                        porteMenu.StartsWith("3") ? 3 :
                                        porteMenu.StartsWith("4") ? 4 :
                                        porteMenu.StartsWith("5") ? 5 : 0;

                            switch (porte)
                            {
                                case 1: porteOpcion = "Alto"; break;
                                case 2: porteOpcion = "Bajo"; break;
                                case 3: porteOpcion = "Dwarf/Compact"; break;
                                case 4: porteOpcion = "Tall"; break;
                                case 5: porteOpcion = "Desconocido"; break;
                            }
                            variedadToUpdate.Porte.nombre_porte = porteOpcion;
                        }
                    }

                    // Tamaño de Grano
                    if (variedadToUpdate.TamanoGrano != null)
                    {
                        var tamanoOpcion = variedadToUpdate.TamanoGrano.nombre_tamano;
                        var tamanoMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de Tamaño (actual: " + tamanoOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. Pequeño",
                                    "2. Mediano",
                                    "3. Grande",
                                    "4. Muy Grande",
                                    "5. Desconocido",
                                    "9. Mantener valor actual"
                                }));

                        if (!tamanoMenu.StartsWith("9"))
                        {
                            int tamano = tamanoMenu.StartsWith("1") ? 1 :
                                         tamanoMenu.StartsWith("2") ? 2 :
                                         tamanoMenu.StartsWith("3") ? 3 :
                                         tamanoMenu.StartsWith("4") ? 4 :
                                         tamanoMenu.StartsWith("5") ? 5 : 0;

                            switch (tamano)
                            {
                                case 1: tamanoOpcion = "Pequeño"; break;
                                case 2: tamanoOpcion = "Mediano"; break;
                                case 3: tamanoOpcion = "Grande"; break;
                                case 4: tamanoOpcion = "Muy Grande"; break;
                                case 5: tamanoOpcion = "Desconocido"; break;
                            }
                            variedadToUpdate.TamanoGrano.nombre_tamano = tamanoOpcion;
                        }
                    }

                    // Altitud Óptima
                    if (variedadToUpdate.AltitudOptima != null)
                    {
                        var altitudOpcion = variedadToUpdate.AltitudOptima.rango_altitud;
                        var altitudMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de Altitud (actual: " + altitudOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. 500-1000 msnm",
                                    "2. 400-900 msnm",
                                    "3. 500-800 msnm",
                                    "4. 700 msnm",
                                    "5. 1200-1800 msnm",
                                    "9. Mantener valor actual"
                                }));

                        if (!altitudMenu.StartsWith("9"))
                        {
                            int altitud = altitudMenu.StartsWith("1") ? 1 :
                                          altitudMenu.StartsWith("2") ? 2 :
                                          altitudMenu.StartsWith("3") ? 3 :
                                          altitudMenu.StartsWith("4") ? 4 :
                                          altitudMenu.StartsWith("5") ? 5 : 0;

                            switch (altitud)
                            {
                                case 1: altitudOpcion = "500-1000 msnm"; break;
                                case 2: altitudOpcion = "400-900 msnm"; break;
                                case 3: altitudOpcion = "500-800 msnm"; break;
                                case 4: altitudOpcion = "700 msnm"; break;
                                case 5: altitudOpcion = "1200-1800 msnm"; break;
                            }
                            variedadToUpdate.AltitudOptima.rango_altitud = altitudOpcion;
                        }
                        variedadToUpdate.AltitudOptima.descripcion = PedirDatoOpcional("Ingrese la nueva descripción de la altitud óptima:", variedadToUpdate.AltitudOptima.descripcion);
                    }

                    // Potencial de Rendimiento
                    if (variedadToUpdate.PotencialRendimiento != null)
                    {
                        var potencialOpcion = variedadToUpdate.PotencialRendimiento.nivel_rendimiento;
                        var potencialMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de Potencial de Rendimiento (actual: " + potencialOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. Bajo (menos de 1500 kg/ha)",
                                    "2. Medio (1500-3000 kg/ha)",
                                    "3. Alto (3000-5000 kg/ha)",
                                    "4. Muy alto (más de 5000 kg/ha)",
                                    "5. Desconocido",
                                    "9. Mantener valor actual"
                                }));

                        if (!potencialMenu.StartsWith("9"))
                        {
                            int potencial = potencialMenu.StartsWith("1") ? 1 :
                                            potencialMenu.StartsWith("2") ? 2 :
                                            potencialMenu.StartsWith("3") ? 3 :
                                            potencialMenu.StartsWith("4") ? 4 :
                                            potencialMenu.StartsWith("5") ? 5 : 0;

                            switch (potencial)
                            {
                                case 1: potencialOpcion = "Bajo (menos de 1500 kg/ha)"; break;
                                case 2: potencialOpcion = "Medio (1500-3000 kg/ha)"; break;
                                case 3: potencialOpcion = "Alto (3000-5000 kg/ha)"; break;
                                case 4: potencialOpcion = "Muy alto (más de 5000 kg/ha)"; break;
                                case 5: potencialOpcion = "Desconocido"; break;
                            }
                            variedadToUpdate.PotencialRendimiento.nivel_rendimiento = potencialOpcion;
                        }
                    }

                    // Calidad de Grano
                    if (variedadToUpdate.CalidadGrano != null)
                    {
                        var calidadOpcion = variedadToUpdate.CalidadGrano.nivel_calidad;
                        var calidadMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de Calidad (actual: " + calidadOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. Excelente",
                                    "2. Muy buena",
                                    "3. Buena",
                                    "4. Regular",
                                    "5. Básica",
                                    "6. Desconocida",
                                    "9. Mantener valor actual"
                                }));

                        if (!calidadMenu.StartsWith("9"))
                        {
                            int calidad = calidadMenu.StartsWith("1") ? 1 :
                                          calidadMenu.StartsWith("2") ? 2 :
                                          calidadMenu.StartsWith("3") ? 3 :
                                          calidadMenu.StartsWith("4") ? 4 :
                                          calidadMenu.StartsWith("5") ? 5 :
                                          calidadMenu.StartsWith("6") ? 6 : 0;

                            switch (calidad)
                            {
                                case 1: calidadOpcion = "Excelente"; break;
                                case 2: calidadOpcion = "Muy buena"; break;
                                case 3: calidadOpcion = "Buena"; break;
                                case 4: calidadOpcion = "Regular"; break;
                                case 5: calidadOpcion = "Básica"; break;
                                case 6: calidadOpcion = "Desconocida"; break;
                            }
                            variedadToUpdate.CalidadGrano.nivel_calidad = calidadOpcion;
                        }
                        variedadToUpdate.CalidadGrano.descripcion = PedirDatoOpcional("Ingrese la nueva descripción de la calidad de grano:", variedadToUpdate.CalidadGrano.descripcion);
                    }

                    // Resistencia (solo el primero)
                    var resistencia = variedadToUpdate.VariedadesResistencia?.FirstOrDefault();
                    if (resistencia != null)
                    {
                        // Tipo de Resistencia
                        var tipoOpcion = resistencia.TipoResistencia?.nombre_tipo ?? "";
                        var tipoMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de Resistencia (actual: " + tipoOpcion + ")[/]")
                                .PageSize(8)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
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
                                    "9. Mantener valor actual"
                                }));

                        if (!tipoMenu.StartsWith("9"))
                        {
                            int tipo = tipoMenu.StartsWith("1") ? 1 :
                                       tipoMenu.StartsWith("2") ? 2 :
                                       tipoMenu.StartsWith("3") ? 3 :
                                       tipoMenu.StartsWith("4") ? 4 :
                                       tipoMenu.StartsWith("5") ? 5 :
                                       tipoMenu.StartsWith("6") ? 6 :
                                       tipoMenu.StartsWith("7") ? 7 :
                                       tipoMenu.StartsWith("8") ? 8 : 0;

                            switch (tipo)
                            {
                                case 1: tipoOpcion = "Roya del cafeto"; break;
                                case 2: tipoOpcion = "Enfermedad de la cereza del café (CBD)"; break;
                                case 3: tipoOpcion = "Nematodos"; break;
                                case 4: tipoOpcion = "Broca del café"; break;
                                case 5: tipoOpcion = "Barrenador del tallo (Xylosandus compactus)"; break;
                                case 6: tipoOpcion = "Marchitez del café (CWD)"; break;
                                case 7: tipoOpcion = "Antracnosis"; break;
                                case 8: tipoOpcion = "Enfermedad de la ampolla roja"; break;
                            }
                            resistencia.TipoResistencia!.nombre_tipo = tipoOpcion;
                        }

                        // Nivel de Resistencia
                        var nivelOpcion = resistencia.NivelResistencia?.nombre_nivel ?? "";
                        var nivelMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de nivel de Resistencia (actual: " + nivelOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. Resistente",
                                    "2. Tolerante",
                                    "3. Susceptible",
                                    "4. Desconocido",
                                    "9. Mantener valor actual"
                                }));

                        if (!nivelMenu.StartsWith("9"))
                        {
                            int nivel = nivelMenu.StartsWith("1") ? 1 :
                                        nivelMenu.StartsWith("2") ? 2 :
                                        nivelMenu.StartsWith("3") ? 3 :
                                        nivelMenu.StartsWith("4") ? 4 : 0;

                            switch (nivel)
                            {
                                case 1: nivelOpcion = "Resistente"; break;
                                case 2: nivelOpcion = "Tolerante"; break;
                                case 3: nivelOpcion = "Susceptible"; break;
                                case 4: nivelOpcion = "Desconocido"; break;
                            }
                            resistencia.NivelResistencia!.nombre_nivel = nivelOpcion;
                        }
                    }

                    // Información Agronómica
                    if (variedadToUpdate.InformacionAgronomica != null)
                    {
                        // Tiempo de cosecha
                        var tiempoOpcion = variedadToUpdate.InformacionAgronomica.tiempo_cosecha;
                        var tiempoMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de tiempo de cosecha (actual: " + tiempoOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. 6-8 meses",
                                    "2. Año 2",
                                    "3. Año 4",
                                    "4. Desconocida",
                                    "9. Mantener valor actual"
                                }));

                        if (!tiempoMenu.StartsWith("9"))
                        {
                            int tiempo = tiempoMenu.StartsWith("1") ? 1 :
                                         tiempoMenu.StartsWith("2") ? 2 :
                                         tiempoMenu.StartsWith("3") ? 3 :
                                         tiempoMenu.StartsWith("4") ? 4 : 0;

                            switch (tiempo)
                            {
                                case 1: tiempoOpcion = "6-8 meses"; break;
                                case 2: tiempoOpcion = "Año 2"; break;
                                case 3: tiempoOpcion = "Año 4"; break;
                                case 4: tiempoOpcion = "Desconocido"; break;
                            }
                            variedadToUpdate.InformacionAgronomica.tiempo_cosecha = tiempoOpcion;
                        }

                        // Maduración
                        var maduracionOpcion = variedadToUpdate.InformacionAgronomica.maduracion;
                        var maduracionMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de maduración (actual: " + maduracionOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. Promedio",
                                    "2. Tardía",
                                    "9. Mantener valor actual"
                                }));

                        if (!maduracionMenu.StartsWith("9"))
                        {
                            int maduracion = maduracionMenu.StartsWith("1") ? 1 :
                                             maduracionMenu.StartsWith("2") ? 2 : 0;

                            switch (maduracion)
                            {
                                case 1: maduracionOpcion = "Promedio"; break;
                                case 2: maduracionOpcion = "Tardía"; break;
                            }
                            variedadToUpdate.InformacionAgronomica.maduracion = maduracionOpcion;
                        }

                        // Nutrición
                        var nutricionOpcion = variedadToUpdate.InformacionAgronomica.nutricion;
                        var nutricionMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de nutrición (actual: " + nutricionOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. Media",
                                    "2. Alta",
                                    "3. Desconocida",
                                    "9. Mantener valor actual"
                                }));

                        if (!nutricionMenu.StartsWith("9"))
                        {
                            int nutricion = nutricionMenu.StartsWith("1") ? 1 :
                                            nutricionMenu.StartsWith("2") ? 2 :
                                            nutricionMenu.StartsWith("3") ? 3 : 0;

                            switch (nutricion)
                            {
                                case 1: nutricionOpcion = "Media"; break;
                                case 2: nutricionOpcion = "Alta"; break;
                                case 3: nutricionOpcion = "Desconocida"; break;
                            }
                            variedadToUpdate.InformacionAgronomica.nutricion = nutricionOpcion;
                        }

                        // Densidad de siembra
                        var densidadOpcion = variedadToUpdate.InformacionAgronomica.densidad_siembra;
                        var densidadMenu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold yellow]Opción de densidad de siembra (actual: " + densidadOpcion + ")[/]")
                                .PageSize(7)
                                .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                                .AddChoices(new[]
                                {
                                    "1. 1000-2000 plantas/ha (usando poda de un solo tallo)",
                                    "2. 2,500 árboles/ha",
                                    "3. hasta 3,000 plantas/ha",
                                    "4. 2000-3000 plantas/ha (usando poda de múltiples tallos)",
                                    "5. hasta 10,000 cafetos/ha",
                                    "9. Mantener valor actual"
                                }));

                        if (!densidadMenu.StartsWith("9"))
                        {
                            int densidad = densidadMenu.StartsWith("1") ? 1 :
                                           densidadMenu.StartsWith("2") ? 2 :
                                           densidadMenu.StartsWith("3") ? 3 :
                                           densidadMenu.StartsWith("4") ? 4 :
                                           densidadMenu.StartsWith("5") ? 5 : 0;

                            switch (densidad)
                            {
                                case 1: densidadOpcion = "1000-2000 plantas/ha (usando poda de un solo tallo)"; break;
                                case 2: densidadOpcion = "2000-3000 plantas/ha (usando poda de múltiples tallos)"; break;
                                case 3: densidadOpcion = "Hasta 3,000 plantas/ha"; break;
                                case 4: densidadOpcion = "2000-3000 plantas/ha (usando poda de múltiples tallos)"; break;
                                case 5: densidadOpcion = "hasta 10,000 cafetos/ha"; break;
                            }
                            variedadToUpdate.InformacionAgronomica.densidad_siembra = densidadOpcion;
                        }
                    }

                    await _VariedadRepository.Update(variedadToUpdate);
                    await _VariedadRepository.SaveAsync();
                    Console.Clear();
                    Console.WriteLine("Variedad actualizada correctamente.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Variedad no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private string PedirDatoOpcional(string mensaje, string valorActual)
        {
            Console.Write($"{mensaje} (actual: {valorActual}): ");
            var entrada = Console.ReadLine();
            return string.IsNullOrWhiteSpace(entrada) ? valorActual : entrada;
        }
 

        }
    }