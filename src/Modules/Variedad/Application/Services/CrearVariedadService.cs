using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Spectre.Console;


namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class CrearVariedadService
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
            AnsiConsole.MarkupLine("[bold cyan]║                      ☕ CREAR NUEVA VARIEDAD DE CAFÉ ☕                            ║[/]");
            AnsiConsole.MarkupLine("[bold cyan]╚══════════════════════════════════════════════════════════════════════════════════╝[/]");
            Console.WriteLine();

            var nombreComun = PedirDatoObligatorio("Ingrese el nombre común de la variedad: ");
            var nombreCientifico = PedirDatoObligatorio("Ingrese el nombre científico de la variedad: ");
            var imagenReferenciaUrl = PedirDatoObligatorio("Ingrese la URL de la imagen de referencia: ");
            var descripcionGeneral = PedirDatoObligatorio("Ingrese la descripción general de la variedad: ");
            var historiaLinaje = PedirDatoObligatorio("Ingrese la historia del linaje de la variedad: ");

            // GRUPO GENÉTICO
            AnsiConsole.MarkupLine("[bold yellow]╔═══════════════════════════════════════════════════════════╗[/]");
            AnsiConsole.MarkupLine("[bold yellow]║                     GRUPO GENETICO                        ║[/]");
            AnsiConsole.MarkupLine("[bold yellow]╚═══════════════════════════════════════════════════════════╝[/]");
            Console.WriteLine();

            

            string grupoOpcion = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de grupo Genetico[/]")
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
                    }));

            int grupo = opcion.StartsWith("1") ? 1 :
                        opcion.StartsWith("2") ? 2 :
                        opcion.StartsWith("3") ? 3 :
                        opcion.StartsWith("4") ? 4 :
                        opcion.StartsWith("5") ? 5 :
                        opcion.StartsWith("6") ? 6 : 9;

            switch (grupo)
            {
                case 1:
                    grupoOpcion = "Arabigo";
                    break;
                case 2:
                    grupoOpcion = "Guinea";
                    break;
                case 3:
                    grupoOpcion = "Congo";
                    break;
                case 4:
                    grupoOpcion = "Uganda";
                    break;
                case 5:
                    grupoOpcion = "Guinea x Congo";
                    break;
                case 6:
                    grupoOpcion = "Guinea x Coffea congensis";
                    break;
                    
            }


            var origen = PedirDatoObligatorio("Ingrese el origen de la variedad: ");

            // DETALLES DEL GRANO
            AnsiConsole.MarkupLine("[bold green]╔════════════════════════════════════════════════╗[/]");
            AnsiConsole.MarkupLine("[bold green]║              DETALLES DEL GRANO                ║[/]");
            AnsiConsole.MarkupLine("[bold green]╚════════════════════════════════════════════════╝[/]");
            Console.WriteLine();

            string PorteOpcion = "";

            opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de Porte[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Alto",
                        "2. Bajo",
                        "3. Dwarf/Compact",
                        "4. Tall",
                        "5. Desconocido",
                    }));

            int porte = opcion.StartsWith("1") ? 1 :
                        opcion.StartsWith("2") ? 2 :
                        opcion.StartsWith("3") ? 3 :
                        opcion.StartsWith("4") ? 4 :
                        opcion.StartsWith("5") ? 5 : 9;

            switch (porte)
            {
                case 1:
                    PorteOpcion = "Alto";
                    break;
                case 2:
                    PorteOpcion = "Bajo";
                    break;
                case 3:
                    PorteOpcion = "Dwarf/Compact";
                    break;
                case 4:
                    PorteOpcion = "Tall";
                    break;
                case 5:
                    PorteOpcion = "Desconocido";
                    break;
                    
            }
             string tamanoOpcion = "";
             opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de Tamaño [/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Pequeño",
                        "2. Mediano",
                        "3. Grande",
                        "4. Muy Grande",
                        "5. Desconocido",
                    }));

                    int tamano = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 :
                                opcion.StartsWith("3") ? 3 :
                                opcion.StartsWith("4") ? 4 :
                                opcion.StartsWith("5") ? 5 : 9;

                    switch (tamano)
                    {
                        case 1:
                            tamanoOpcion = "Pequeño";
                            break;
                        case 2:
                            tamanoOpcion = "Mediano";
                            break;
                        case 3:
                            tamanoOpcion = "Grande";
                            break;
                        case 4:
                            tamanoOpcion = "Muy Grande";
                            break;
                        case 5:
                            tamanoOpcion = "Desconocido";
                            break;

                            
                    }
                string altitudOpcion = "";
                opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de Altitud [/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. 500-1000 msnm",
                        "2. 400-900 msnm",
                        "3. 500-800 msnm",
                        "4. 700 msnm",
                        "5. 1200-1800 msnm"
                    }));

                    int altitud = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 :
                                opcion.StartsWith("3") ? 3 :
                                opcion.StartsWith("4") ? 4 :
                                opcion.StartsWith("5") ? 5 : 9;

                    switch (altitud)
                    {
                        case 1:
                            altitudOpcion = "500-1000 msnm";
                            break;
                        case 2:
                            altitudOpcion = "400-900 msnm";
                            break;
                        case 3:
                            altitudOpcion = "500-800 msnm";
                            break;
                        case 4:
                            altitudOpcion = "700 msnm";
                            break;
                        case 5:
                            altitudOpcion = "1200-1800 msnm";
                            break;

                            
                    }
            var descripcionAltitudOptima = PedirDatoObligatorio("Ingrese la descripción de la altitud óptima: ");
            var potencialRendimiento = PedirDatoObligatorio("Ingrese el potencial de rendimiento: ");
            string potencialOpcion = "";
                opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de Potencial de Rendimiento [/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                       "1. Bajo (menos de 1500 kg/ha)",
                        "2. Medio (1500-3000 kg/ha)",
                        "3. Alto (3000-5000 kg/ha)",
                        "4. Muy alto (más de 5000 kg/ha)",
                        "5. Desconocido"
                    }));

                    int potencial = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 :
                                opcion.StartsWith("3") ? 3 :
                                opcion.StartsWith("4") ? 4 :
                                opcion.StartsWith("5") ? 5 : 9;

                    switch (potencial)
                    {
                        case 1:
                            potencialOpcion = "Bajo (menos de 1500 kg/ha)";
                            break;
                        case 2:
                            potencialOpcion = "Medio (1500-3000 kg/ha)";
                            break;
                        case 3:
                            potencialOpcion = "Alto (3000-5000 kg/ha)";
                            break;
                        case 4:
                            potencialOpcion = "Muy alto (más de 5000 kg/ha)";
                            break;
                        case 5:
                            potencialOpcion = "Desconocido";
                            break;

                    }
  
            string calidadOpcion = "";
                opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de Calidad [/]")
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
                    }));

                    int calidad = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 :
                                opcion.StartsWith("3") ? 3 :
                                opcion.StartsWith("4") ? 4 :
                                opcion.StartsWith("5") ? 5 :
                                opcion.StartsWith("6") ? 6 : 9;

            switch (calidad)
            {
                case 1:
                    calidadOpcion = "Excelente";
                    break;
                case 2:
                    calidadOpcion = "Muy buena";
                    break;
                case 3:
                    calidadOpcion = "Buena";
                    break;
                case 4:
                    calidadOpcion = "Regular";
                    break;
                case 5:
                    calidadOpcion = "Básica";
                    break;
                case 6:
                    calidadOpcion = "Desconocida";
                    break;

            }


            var descripcionCalidadGrano = PedirDatoObligatorio("Descripcion de la calidad de grano: ");
           
            string ResistenciaOpcion = "";
                opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de Resistencia [/]")
                    .PageSize(7)
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
                    }));

                    int resistencia = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 :
                                opcion.StartsWith("3") ? 3 :
                                opcion.StartsWith("4") ? 4 :
                                opcion.StartsWith("5") ? 5 :
                                opcion.StartsWith("6") ? 6 : 
                                opcion.StartsWith("7") ? 7 : 
                                opcion.StartsWith("8") ? 8 : 9;

            switch (resistencia)
            {
                case 1:
                    ResistenciaOpcion = "Roya del cafeto";
                    break;
                case 2:
                    ResistenciaOpcion = "Enfermedad de la cereza del café (CBD)";
                    break;
                case 3:
                    ResistenciaOpcion = "Nematodos";
                    break;
                case 4:
                    ResistenciaOpcion = "Broca del café";
                    break;
                case 5:
                    ResistenciaOpcion = "Barrenador del tallo (Xylosandus compactus)";
                    break;
                case 6:
                    ResistenciaOpcion = "Marchitez del café (CWD)";
                    break;
                case 7:
                    ResistenciaOpcion = "Antracnosis";
                    break;
                case 8:
                    ResistenciaOpcion = "Enfermedad de la ampolla roja";
                    break;

            }

 

             string nivelOpcion = "";
                opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de nivel de Resistencia [/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Resistente",
                        "2. Tolerante",
                        "3. Susceptible",
                        "4. Desconocido",
                    }));

                    int nivel = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 :
                                opcion.StartsWith("3") ? 3 :
                                opcion.StartsWith("4") ? 4 : 9;

            switch (nivel)
            {
                case 1:
                    nivelOpcion = "Resistente";
                    break;
                case 2:
                    nivelOpcion = "Tolerante";
                    break;
                case 3:
                    nivelOpcion = "Susceptible";
                    break;
                case 4:
                    nivelOpcion = "Desconocido";
                    break;
            }


            // INFORMACION AGRONOMICA
            AnsiConsole.MarkupLine("[bold blue]╔════════════════════════════════════════════════════════════════════════╗[/]");
            AnsiConsole.MarkupLine("[bold blue]║                      INFORMACION AGRONOMICA                            ║[/]");
            AnsiConsole.MarkupLine("[bold blue]╚════════════════════════════════════════════════════════════════════════╝[/]");
            Console.WriteLine();

            string tiempoOpcion = "";
                opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de tiempo de cosecha [/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. 6-8 meses",
                        "2. Año 2",
                        "3. Año 4",
                        "4. Desconocida"
                    }));

                    int tiempo = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 :
                                opcion.StartsWith("3") ? 3 :
                                opcion.StartsWith("4") ? 4 : 9;

            switch (tiempo)
            {
                case 1:
                    tiempoOpcion = "6-8 meses";
                    break;
                case 2:
                    tiempoOpcion = "Año 2";
                    break;
                case 3:
                    tiempoOpcion = "Año 4";
                    break;
                case 4:
                    tiempoOpcion = "Desconocido";
                    break;
            }



            string maduracionOpcion = "";
                opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de maduración [/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Promedio",
                        "2. Tardía",
   
                    }));

                int maduracion = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 : 9;

            switch (maduracion)
            {
                case 1:
                    maduracionOpcion = "Promedio";
                    break;
                case 2:
                    maduracionOpcion = "Tardía";
                    break;
 
            }
            

            string nutricionOpcion = "";
                opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de nutricion [/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Media",
                        "2. Alta",
                        "3. Desconocida",
   
                    }));

                int nutricion = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 :
                                opcion.StartsWith("3") ? 3 : 9;

            switch (nutricion)
            {
                case 1:
                    nutricionOpcion = "Media";
                    break;
                case 2:
                    nutricionOpcion = "Alta";
                    break;
                case 3:
                    nutricionOpcion = "Desconocida";
                    break;
 
            }
            
            string densidadOpcion = "";
                opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Opcion de densidad de siembra [/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. 1000-2000 plantas/ha (usando poda de un solo tallo)",
                        "2. 2,500 árboles/ha",
                        "3. hasta 3,000 plantas/ha",
                        "4. 2000-3000 plantas/ha (usando poda de múltiples tallos)",
                        "5. hasta 10,000 cafetos/ha"
   
                    }));

                int densidad = opcion.StartsWith("1") ? 1 :
                                opcion.StartsWith("2") ? 2 :
                                opcion.StartsWith("3") ? 3 :
                                opcion.StartsWith("4") ? 4 :
                                opcion.StartsWith("5") ? 5 : 9;

            switch (densidad)
            {
                case 1:
                    densidadOpcion = "1000-2000 plantas/ha (usando poda de un solo tallo)";
                    break;
                case 2:
                    densidadOpcion = "2000-3000 plantas/ha (usando poda de múltiples tallos)";
                    break;
                case 3:
                    densidadOpcion = "Hasta 3,000 plantas/ha";
                    break;
                case 4:
                    densidadOpcion = "2000-3000 plantas/ha (usando poda de múltiples tallos)";
                    break;
                case 5:
                    densidadOpcion = "hasta 10,000 cafetos/ha";
                    break;
 
            }

            Console.Write("Ingrese el rango de Altitud: ");
            var rangoAltitud = Console.ReadLine();

            // VariedadResistencia

            // CREACIÓN DE OBJETOS (igual a tu código original)
            var nuevaVariedad = new Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad
            {
                nombre_comun = nombreComun,
                nombre_cientifico = nombreCientifico,
                descripcion_general = descripcionGeneral,
                historia_linaje = historiaLinaje,
                imagen_referencia_url = imagenReferenciaUrl
            };

            var nuevoGrupoGenetico = new Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities.GrupoGenetico { nombre_grupo = grupoOpcion };
            var nuevoPorte = new Cafe_Colombiano.src.Modules.Porte.Domain.Entities.Porte { nombre_porte = PorteOpcion };
            var nuevoTamanoGrano = new Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities.TamanoGrano { nombre_tamano = tamanoOpcion };
            var nuevaAltitudOptima = new Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities.AltitudOptima { rango_altitud = altitudOpcion, descripcion = descripcionAltitudOptima };
            var nuevoPotencialRendimiento = new Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities.PotencialRendimiento { nivel_rendimiento = potencialOpcion };
            var nuevaCalidadGrano = new Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities.CalidadGrano { nivel_calidad = calidadOpcion };
            var nuevoTipoResistencia = new Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities.TipoResistencia { nombre_tipo = ResistenciaOpcion };
            var nuevoNivelResistencia = new Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities.NivelResistencia { nombre_nivel = nivelOpcion };

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
            nuevaAltitudOptima.rango_altitud = rangoAltitud;

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
    }
}