using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Spectre.Console;
using System.Linq;

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
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var variedadToUpdate = await _VariedadRepository.GetVariedadByIdAsync(id);
            if (variedadToUpdate == null)
            {
                Console.Clear();
                Console.WriteLine("Variedad no encontrada.");
                return;
            }

            // Datos principales
            variedadToUpdate.nombre_comun = PedirDatoOpcional("Ingrese el nuevo nombre común de la variedad:", variedadToUpdate.nombre_comun ?? string.Empty) ;
            variedadToUpdate.nombre_cientifico = PedirDatoOpcional("Ingrese el nuevo nombre científico de la variedad:", variedadToUpdate.nombre_cientifico ?? string.Empty);
            variedadToUpdate.descripcion_general = PedirDatoOpcional("Ingrese la nueva descripción de la variedad:", variedadToUpdate.descripcion_general ?? string.Empty);
            variedadToUpdate.historia_linaje = PedirDatoOpcional("Ingrese la nueva historia del linaje de la variedad:", variedadToUpdate.historia_linaje ?? string.Empty);
            variedadToUpdate.imagen_referencia_url = PedirDatoOpcional("Ingrese la nueva URL de la imagen de referencia de la variedad:", variedadToUpdate.imagen_referencia_url ?? string.Empty);

            // Grupo Genético
            if (variedadToUpdate.GrupoGenetico != null)
            {
                string[] opcionesGrupo = { "1. Arábigo", "2. Guinea", "3. Congo", "4. Uganda", "5. Guinea x Congo", "6. Guinea x Coffea congensis" };
                string[] valoresGrupo = { "Arabigo", "Guinea", "Congo", "Uganda", "Guinea x Congo", "Guinea x Coffea congensis" };
                var grupoOpcion = await SeleccionarOpcionAsync("[bold yellow]Opción de grupo Genético[/]", opcionesGrupo, valoresGrupo, variedadToUpdate.GrupoGenetico.nombre_grupo ?? string.Empty,
                    _VariedadRepository.GetGrupoGeneticoByNombreAsync,
                    nombre => new Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities.GrupoGenetico { nombre_grupo = nombre });
                variedadToUpdate.GrupoGenetico = grupoOpcion;
                variedadToUpdate.GrupoGenetico.origen = PedirDatoOpcional("Ingrese el nuevo origen de la variedad:", variedadToUpdate.GrupoGenetico.origen ?? string.Empty);
            }

            // Porte
            if (variedadToUpdate.Porte != null)
            {
                string[] opcionesPorte = { "1. Alto", "2. Bajo", "3. Dwarf/Compact", "4. Tall", "5. Desconocido" };
                string[] valoresPorte = { "Alto", "Bajo", "Dwarf/Compact", "Tall", "Desconocido" };
                var porteOpcion = await SeleccionarOpcionAsync("[bold yellow]Opción de Porte[/]", opcionesPorte, valoresPorte, variedadToUpdate.Porte.nombre_porte ?? string.Empty,
                    _VariedadRepository.GetPorteByNombreAsync,
                    nombre => new Cafe_Colombiano.src.Modules.Porte.Domain.Entities.Porte { nombre_porte = nombre });
                variedadToUpdate.Porte = porteOpcion;
            }

            // Tamaño de Grano
            if (variedadToUpdate.TamanoGrano != null)
            {
                string[] opcionesTamano = { "1. Pequeño", "2. Mediano", "3. Grande", "4. Muy Grande", "5. Desconocido" };
                string[] valoresTamano = { "Pequeño", "Mediano", "Grande", "Muy Grande", "Desconocido" };
                var tamanoOpcion = await SeleccionarOpcionAsync("[bold yellow]Opción de Tamaño[/]", opcionesTamano, valoresTamano, variedadToUpdate.TamanoGrano.nombre_tamano ?? string.Empty,
                    _VariedadRepository.GetTamanoGranoByNombreAsync,
                    nombre => new Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities.TamanoGrano { nombre_tamano = nombre });
                variedadToUpdate.TamanoGrano = tamanoOpcion;
            }

            // Altitud Óptima
            if (variedadToUpdate.AltitudOptima != null)
            {
                string[] opcionesAltitud = { "1. 500-1000 msnm", "2. 400-900 msnm", "3. 500-800 msnm", "4. 700 msnm", "5. 1200-1800 msnm" };
                string[] valoresAltitud = { "500-1000 msnm", "400-900 msnm", "500-800 msnm", "700 msnm", "1200-1800 msnm" };
                var altitudOpcion = await SeleccionarOpcionAsync("[bold yellow]Opción de Altitud[/]", opcionesAltitud, valoresAltitud, variedadToUpdate.AltitudOptima.rango_altitud ?? string.Empty,
                    _VariedadRepository.GetAltitudOptimaByRangoAsync,
                    rango => new Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities.AltitudOptima { rango_altitud = rango });
                variedadToUpdate.AltitudOptima = altitudOpcion;
                variedadToUpdate.AltitudOptima.descripcion = PedirDatoOpcional("Ingrese la nueva descripción de la altitud óptima:", variedadToUpdate.AltitudOptima.descripcion ?? string.Empty );
            }

            // Potencial de Rendimiento
            if (variedadToUpdate.PotencialRendimiento != null)
            {
                string[] opcionesPotencial = { "1. Bajo (menos de 1500 kg/ha)", "2. Medio (1500-3000 kg/ha)", "3. Alto (3000-5000 kg/ha)", "4. Muy alto (más de 5000 kg/ha)", "5. Desconocido" };
                string[] valoresPotencial = { "Bajo (menos de 1500 kg/ha)", "Medio (1500-3000 kg/ha)", "Alto (3000-5000 kg/ha)", "Muy alto (más de 5000 kg/ha)", "Desconocido" };
                var potencialOpcion = await SeleccionarOpcionAsync("[bold yellow]Opción de Potencial de Rendimiento[/]", opcionesPotencial, valoresPotencial, variedadToUpdate.PotencialRendimiento.nivel_rendimiento ?? string.Empty,
                    _VariedadRepository.GetPotencialRendimientoByNivelAsync,
                    nivel => new Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities.PotencialRendimiento { nivel_rendimiento = nivel });
                variedadToUpdate.PotencialRendimiento = potencialOpcion;
            }

            // Calidad de Grano
            if (variedadToUpdate.CalidadGrano != null)
            {
                string[] opcionesCalidad = { "1. Excelente", "2. Muy buena", "3. Buena", "4. Regular", "5. Básica", "6. Desconocida" };
                string[] valoresCalidad = { "Excelente", "Muy buena", "Buena", "Regular", "Básica", "Desconocida" };
                var calidadOpcion = await SeleccionarOpcionAsync("[bold yellow]Opción de Calidad[/]", opcionesCalidad, valoresCalidad, variedadToUpdate.CalidadGrano.nivel_calidad ?? string.Empty,
                    _VariedadRepository.GetCalidadGranoByNivelAsync,
                    nivel => new Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities.CalidadGrano { nivel_calidad = nivel });
                variedadToUpdate.CalidadGrano = calidadOpcion;
                variedadToUpdate.CalidadGrano.descripcion = PedirDatoOpcional("Ingrese la nueva descripción de la calidad de grano:", variedadToUpdate.CalidadGrano.descripcion ?? string.Empty);
            }

            // Resistencia (solo el primero)
            await ActualizarResistenciaAsync(variedadToUpdate);

            // Información Agronómica
            if (variedadToUpdate.InformacionAgronomica != null)
            {
                string[] opcionesTiempo = { "1. 6-8 meses", "2. Año 2", "3. Año 4", "4. Desconocida" };
                string[] valoresTiempo = { "6-8 meses", "Año 2", "Año 4", "Desconocido" };
                variedadToUpdate.InformacionAgronomica.tiempo_cosecha = SeleccionarOpcion("[bold yellow]Opción de tiempo de cosecha[/]", opcionesTiempo, valoresTiempo, variedadToUpdate.InformacionAgronomica.tiempo_cosecha ?? string.Empty);

                string[] opcionesMaduracion = { "1. Promedio", "2. Tardía" };
                string[] valoresMaduracion = { "Promedio", "Tardía" };
                variedadToUpdate.InformacionAgronomica.maduracion = SeleccionarOpcion("[bold yellow]Opción de maduración[/]", opcionesMaduracion, valoresMaduracion, variedadToUpdate.InformacionAgronomica.maduracion ?? string.Empty);

                string[] opcionesNutricion = { "1. Media", "2. Alta", "3. Desconocida" };
                string[] valoresNutricion = { "Media", "Alta", "Desconocida" };
                variedadToUpdate.InformacionAgronomica.nutricion = SeleccionarOpcion("[bold yellow]Opción de nutrición[/]", opcionesNutricion, valoresNutricion, variedadToUpdate.InformacionAgronomica.nutricion ?? string.Empty);

                string[] opcionesDensidad = {
                    "1. 1000-2000 plantas/ha (usando poda de un solo tallo)",
                    "2. 2,500 árboles/ha",
                    "3. hasta 3,000 plantas/ha",
                    "4. 2000-3000 plantas/ha (usando poda de múltiples tallos)",
                    "5. hasta 10,000 cafetos/ha"
                };
                string[] valoresDensidad = {
                    "1000-2000 plantas/ha (usando poda de un solo tallo)",
                    "2,500 árboles/ha",
                    "hasta 3,000 plantas/ha",
                    "2000-3000 plantas/ha (usando poda de múltiples tallos)",
                    "hasta 10,000 cafetos/ha"
                };
                variedadToUpdate.InformacionAgronomica.densidad_siembra = SeleccionarOpcion("[bold yellow]Opción de densidad de siembra[/]", opcionesDensidad, valoresDensidad, variedadToUpdate.InformacionAgronomica.densidad_siembra ?? string.Empty);
            }

            await _VariedadRepository.Update(variedadToUpdate);
            await _VariedadRepository.SaveAsync();
            Console.Clear();
            Console.WriteLine("Variedad actualizada correctamente.");
        }

        private string PedirDatoOpcional(string mensaje, string valorActual)
        {
            Console.Write($"{mensaje} (actual: {valorActual}): ");
            var entrada = Console.ReadLine();
            return string.IsNullOrWhiteSpace(entrada) ? valorActual : entrada;
        }

        private string SeleccionarOpcion(string titulo, string[] opciones, string[] valores, string valorActual)
        {
            var opcionesMenu = opciones.Concat(new[] { "9. Mantener valor actual" }).ToArray();
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"{titulo} (actual: {valorActual})")
                    .PageSize(Math.Max(opcionesMenu.Length, 3))
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(opcionesMenu)
            );
            if (opcion.StartsWith("9"))
                return valorActual;
            int idx = Array.FindIndex(opcionesMenu, o => o == opcion);
            return valores[idx];
        }

        private async Task<T> SeleccionarOpcionAsync<T>(
            string titulo,
            string[] opciones,
            string[] valores,
            string valorActual,
            Func<string, Task<T?>> buscarExistente,
            Func<string, T> crearNuevo
        ) where T : class
        {
            var seleccion = SeleccionarOpcion(titulo, opciones, valores, valorActual);
            if (seleccion == valorActual)
                return await buscarExistente(valorActual) ?? crearNuevo(valorActual);

            var existente = await buscarExistente(seleccion);
            return existente ?? crearNuevo(seleccion);
        }

        private async Task ActualizarResistenciaAsync(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad variedadToUpdate)
        {
            var resistencia = variedadToUpdate.VariedadesResistencia?.FirstOrDefault();
            if (resistencia == null) return;

            string[] opcionesTipo = {
                "1. Roya del cafeto",
                "2. Enfermedad de la cereza del café (CBD)",
                "3. Nematodos",
                "4. Broca del café",
                "5. Barrenador del tallo (Xylosandus compactus)",
                "6. Marchitez del café (CWD)",
                "7. Antracnosis",
                "8. Enfermedad de la ampolla roja"
            };
            string[] valoresTipo = {
                "Roya del cafeto",
                "Enfermedad de la cereza del café (CBD)",
                "Nematodos",
                "Broca del café",
                "Barrenador del tallo (Xylosandus compactus)",
                "Marchitez del café (CWD)",
                "Antracnosis",
                "Enfermedad de la ampolla roja"
            };
            string tipoActual = resistencia.TipoResistencia?.nombre_tipo ?? "";
            var nuevoTipo = await SeleccionarOpcionAsync("[bold yellow]Opción de Resistencia[/]", opcionesTipo, valoresTipo, tipoActual,
                _VariedadRepository.GetTipoResistenciaByNombreAsync,
                nombre => new Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities.TipoResistencia { nombre_tipo = nombre });

            string[] opcionesNivel = { "1. Resistente", "2. Tolerante", "3. Susceptible", "4. Desconocido" };
            string[] valoresNivel = { "Resistente", "Tolerante", "Susceptible", "Desconocido" };
            string nivelActual = resistencia.NivelResistencia?.nombre_nivel ?? "";
            var nuevoNivel = await SeleccionarOpcionAsync("[bold yellow]Opción de nivel de Resistencia[/]", opcionesNivel, valoresNivel, nivelActual,
                _VariedadRepository.GetNivelResistenciaByNombreAsync,
                nombre => new Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities.NivelResistencia { nombre_nivel = nombre });

            // Si el usuario cambió tipo o nivel, elimina y crea la relación
            if (nuevoTipo.nombre_tipo != tipoActual || nuevoNivel.nombre_nivel != nivelActual)
            {
                variedadToUpdate.VariedadesResistencia!.Remove(resistencia);
                await _VariedadRepository.Update(variedadToUpdate);
                await _VariedadRepository.SaveAsync();

                var nuevaResistencia = new Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities.VariedadResistencia
                {
                    TipoResistencia = nuevoTipo,
                    NivelResistencia = nuevoNivel
                };
                variedadToUpdate.VariedadesResistencia.Add(nuevaResistencia);
            }
        }
    }
}