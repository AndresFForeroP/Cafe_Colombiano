using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Spectre.Console;


namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class CrearVariedadService : ICrearVariedadServices
    {
        private readonly IVariedadRepository _variedadService;

        public CrearVariedadService(IVariedadRepository variedadService)
        {
            _variedadService = variedadService;
        }


        public async Task CrearVariedad()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold cyan]╔══════════════════════════════════════════════════════════════════════════════════╗[/]");
            AnsiConsole.MarkupLine("[bold cyan]║                      ☕ CREAR NUEVA VARIEDAD DE CAFÉ ☕                          ║[/]");
            AnsiConsole.MarkupLine("[bold cyan]╚══════════════════════════════════════════════════════════════════════════════════╝[/]");
            Console.WriteLine();

            var nombreComun = PedirDatoObligatorio("Ingrese el nombre común de la variedad: ");
            var nombreCientifico = PedirDatoObligatorio("Ingrese el nombre científico de la variedad: ");
            var imagenReferenciaUrl = PedirDatoObligatorio("Ingrese la URL de la imagen de referencia: ");
            var descripcionGeneral = PedirDatoObligatorio("Ingrese la descripción general de la variedad: ");
            var historiaLinaje = PedirDatoObligatorio("Ingrese la historia del linaje de la variedad: ");

            // GRUPO GENÉTICO
            string grupoOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de grupo Genético[/]",
                new[] { "1. Arábigo", "2. Guinea", "3. Congo", "4. Uganda", "5. Guinea x Congo", "6. Guinea x Coffea congensis" },
                new[] { "Arabigo", "Guinea", "Congo", "Uganda", "Guinea x Congo", "Guinea x Coffea congensis" }
            );

            var origen = PedirDatoObligatorio("Ingrese el origen de la variedad: ");

            // PORTE
            string porteOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de Porte[/]",
                new[] { "1. Alto", "2. Bajo", "3. Dwarf/Compact", "4. Tall", "5. Desconocido" },
                new[] { "Alto", "Bajo", "Dwarf/Compact", "Tall", "Desconocido" }
            );

            // TAMAÑO DE GRANO
            string tamanoOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de Tamaño[/]",
                new[] { "1. Pequeño", "2. Mediano", "3. Grande", "4. Muy Grande", "5. Desconocido" },
                new[] { "Pequeño", "Mediano", "Grande", "Muy Grande", "Desconocido" }
            );

            // ALTITUD ÓPTIMA
            string altitudOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de Altitud[/]",
                new[] { "1. 500-1000 msnm", "2. 400-900 msnm", "3. 500-800 msnm", "4. 700 msnm", "5. 1200-1800 msnm" },
                new[] { "500-1000 msnm", "400-900 msnm", "500-800 msnm", "700 msnm", "1200-1800 msnm" }
            );
            var descripcionAltitudOptima = PedirDatoObligatorio("Ingrese la descripción de la altitud óptima: ");

            // POTENCIAL DE RENDIMIENTO
            string potencialOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de Potencial de Rendimiento[/]",
                new[] { "1. Bajo (menos de 1500 kg/ha)", "2. Medio (1500-3000 kg/ha)", "3. Alto (3000-5000 kg/ha)", "4. Muy alto (más de 5000 kg/ha)", "5. Desconocido" },
                new[] { "Bajo (menos de 1500 kg/ha)", "Medio (1500-3000 kg/ha)", "Alto (3000-5000 kg/ha)", "Muy alto (más de 5000 kg/ha)", "Desconocido" }
            );

            // CALIDAD DE GRANO
            string calidadOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de Calidad[/]",
                new[] { "1. Excelente", "2. Muy buena", "3. Buena", "4. Regular", "5. Básica", "6. Desconocida" },
                new[] { "Excelente", "Muy buena", "Buena", "Regular", "Básica", "Desconocida" }
            );
            var descripcionCalidadGrano = PedirDatoObligatorio("Descripción de la calidad de grano: ");

            // RESISTENCIA
            string resistenciaOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de Resistencia[/]",
                new[] { "1. Roya del cafeto", "2. Enfermedad de la cereza del café (CBD)", "3. Nematodos", "4. Broca del café", "5. Barrenador del tallo (Xylosandus compactus)", "6. Marchitez del café (CWD)", "7. Antracnosis", "8. Enfermedad de la ampolla roja" },
                new[] { "Roya del cafeto", "Enfermedad de la cereza del café (CBD)", "Nematodos", "Broca del café", "Barrenador del tallo (Xylosandus compactus)", "Marchitez del café (CWD)", "Antracnosis", "Enfermedad de la ampolla roja" }
            );

            // NIVEL DE RESISTENCIA
            string nivelOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de nivel de Resistencia[/]",
                new[] { "1. Resistente", "2. Tolerante", "3. Susceptible", "4. Desconocido" },
                new[] { "Resistente", "Tolerante", "Susceptible", "Desconocido" }
            );

            // INFORMACIÓN AGRONÓMICA
            string tiempoOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de tiempo de cosecha[/]",
                new[] { "1. 6-8 meses", "2. Año 2", "3. Año 4", "4. Desconocida" },
                new[] { "6-8 meses", "Año 2", "Año 4", "Desconocido" }
            );

            string maduracionOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de maduración[/]",
                new[] { "1. Promedio", "2. Tardía" },
                new[] { "Promedio", "Tardía" }
            );

            string nutricionOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de nutrición[/]",
                new[] { "1. Media", "2. Alta", "3. Desconocida" },
                new[] { "Media", "Alta", "Desconocida" }
            );

            string densidadOpcion = SeleccionarOpcion(
                "[bold yellow]Opción de densidad de siembra[/]",
                new[] { "1. 1000-2000 plantas/ha (usando poda de un solo tallo)", "2. 2,500 árboles/ha", "3. hasta 3,000 plantas/ha", "4. 2000-3000 plantas/ha (usando poda de múltiples tallos)", "5. hasta 10,000 cafetos/ha" },
                new[] { "1000-2000 plantas/ha (usando poda de un solo tallo)", "2,500 árboles/ha", "hasta 3,000 plantas/ha", "2000-3000 plantas/ha (usando poda de múltiples tallos)", "hasta 10,000 cafetos/ha" }
            );


            // VariedadResistencia

            // CREACIÓN DE OBJETOS
            var nuevaVariedad = new Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad
            {
                nombre_comun = nombreComun,
                nombre_cientifico = nombreCientifico,
                descripcion_general = descripcionGeneral,
                historia_linaje = historiaLinaje,
                imagen_referencia_url = imagenReferenciaUrl
            };

            // GRUPO GENÉTICO
            var grupoExistente = await _variedadService.GetGrupoGeneticoByNombreAsync(grupoOpcion);
            var nuevoGrupoGenetico = grupoExistente ?? new Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities.GrupoGenetico { nombre_grupo = grupoOpcion };
            nuevoGrupoGenetico.origen = origen;

            // PORTE
            var porteExistente = await _variedadService.GetPorteByNombreAsync(porteOpcion);
            var nuevoPorte = porteExistente ?? new Cafe_Colombiano.src.Modules.Porte.Domain.Entities.Porte { nombre_porte = porteOpcion };

            // TAMAÑO DE GRANO
            var tamanoExistente = await _variedadService.GetTamanoGranoByNombreAsync(tamanoOpcion);
            var nuevoTamanoGrano = tamanoExistente ?? new Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities.TamanoGrano { nombre_tamano = tamanoOpcion };

            // ALTITUD ÓPTIMA
            var altitudExistente = await _variedadService.GetAltitudOptimaByRangoAsync(altitudOpcion);
            var nuevaAltitudOptima = altitudExistente ?? new Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities.AltitudOptima { rango_altitud = altitudOpcion, descripcion = descripcionAltitudOptima };

            // POTENCIAL DE RENDIMIENTO
            var potencialExistente = await _variedadService.GetPotencialRendimientoByNivelAsync(potencialOpcion);
            var nuevoPotencialRendimiento = potencialExistente ?? new Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities.PotencialRendimiento { nivel_rendimiento = potencialOpcion };

            // CALIDAD DE GRANO
            var calidadExistente = await _variedadService.GetCalidadGranoByNivelAsync(calidadOpcion);
            var nuevaCalidadGrano = calidadExistente ?? new Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities.CalidadGrano { nivel_calidad = calidadOpcion };
            nuevaCalidadGrano.descripcion = descripcionCalidadGrano;

            // TIPO DE RESISTENCIA
            var tipoResistenciaExistente = await _variedadService.GetTipoResistenciaByNombreAsync(resistenciaOpcion);
            var nuevoTipoResistencia = tipoResistenciaExistente ?? new Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities.TipoResistencia { nombre_tipo = resistenciaOpcion };

            // NIVEL DE RESISTENCIA
            var nivelResistenciaExistente = await _variedadService.GetNivelResistenciaByNombreAsync(nivelOpcion);
            var nuevoNivelResistencia = nivelResistenciaExistente ?? new Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities.NivelResistencia { nombre_nivel = nivelOpcion };

            // INFORMACIÓN AGRONÓMICA (no suele ser única, así que se puede crear siempre)
            var nuevaInformacionAgronomica = new Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities.InformacionAgronomica
            {
                tiempo_cosecha = tiempoOpcion,
                maduracion = maduracionOpcion,
                nutricion = nutricionOpcion,
                densidad_siembra = densidadOpcion
            };

            // Asignaciones
            nuevaVariedad.GrupoGenetico = nuevoGrupoGenetico;
            nuevaVariedad.GrupoGenetico.origen = origen;
            nuevaVariedad.Porte = nuevoPorte;
            nuevaVariedad.TamanoGrano = nuevoTamanoGrano;
            nuevaVariedad.AltitudOptima = nuevaAltitudOptima;
            nuevaVariedad.PotencialRendimiento = nuevoPotencialRendimiento;
            nuevaVariedad.CalidadGrano = nuevaCalidadGrano;
            nuevaCalidadGrano.descripcion = descripcionCalidadGrano;
            nuevaVariedad.InformacionAgronomica = nuevaInformacionAgronomica;
            nuevaAltitudOptima.rango_altitud = altitudOpcion;

            nuevaVariedad.VariedadesResistencia!.Add(new Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities.VariedadResistencia
            {
                Variedad = nuevaVariedad,
                TipoResistencia = nuevoTipoResistencia,
                NivelResistencia = nuevoNivelResistencia
            });

            await _variedadService.Add(nuevaVariedad);
            await _variedadService.SaveAsync();

            AnsiConsole.MarkupLine("[bold green]✔ Variedad creada exitosamente.[/]");
        }





        private string PedirDatoObligatorio(string mensaje)
        {
            string? dato;
            do
            {
                Console.Write(mensaje);
                dato = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dato))
                {
                    Console.WriteLine("Este campo es obligatorio.");
                }
            } while (string.IsNullOrWhiteSpace(dato));

            return dato;
        }

        private string SeleccionarOpcion(string titulo, string[] opciones, string[] valores)
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(titulo)
                    .PageSize(Math.Max(5, opciones.Length))
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(opciones)
    );
    int idx = Array.FindIndex(opciones, o => o == opcion);
    return valores[idx];
}
    }
}