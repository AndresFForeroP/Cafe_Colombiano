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

            var grupoGenetico = PedirDatoObligatorio("Ingrese el grupo genético de la variedad: ");
            var origen = PedirDatoObligatorio("Ingrese el origen de la variedad: ");

            // DETALLES DEL GRANO
            AnsiConsole.MarkupLine("[bold green]╔════════════════════════════════════════════════╗[/]");
            AnsiConsole.MarkupLine("[bold green]║              DETALLES DEL GRANO                ║[/]");
            AnsiConsole.MarkupLine("[bold green]╚════════════════════════════════════════════════╝[/]");
            Console.WriteLine();

            var porte = PedirDatoObligatorio("Ingrese el porte de la planta(Alto/Bajo/Dwarf/Compact/Tall/Desconocido): ");
            var tamanoGrano = PedirDatoObligatorio("Ingrese el tamaño de grano(Pequeño/Mediano/Grande/Muy Grande): ");
            var altitudOptima = PedirDatoObligatorio("Ingrese la altitud óptima(ej: 500 - 1000): ");
            var descripcionAltitudOptima = PedirDatoObligatorio("Ingrese la descripción de la altitud óptima: ");
            var potencialRendimiento = PedirDatoObligatorio("Ingrese el potencial de rendimiento: ");
            var calidadGrano = PedirDatoObligatorio("Ingrese la calidad de grano: ");
            var descripcionCalidadGrano = PedirDatoObligatorio("Descripcion de la calidad de grano: ");
            var tipoResistencia = PedirDatoObligatorio("Tipo de resistencia: ");
            var nivelResistencia = PedirDatoObligatorio("Nivel de resistencia: ");

            // INFORMACION AGRONOMICA
            AnsiConsole.MarkupLine("[bold blue]╔════════════════════════════════════════════════════════════════════════╗[/]");
            AnsiConsole.MarkupLine("[bold blue]║                      INFORMACION AGRONOMICA                            ║[/]");
            AnsiConsole.MarkupLine("[bold blue]╚════════════════════════════════════════════════════════════════════════╝[/]");
            Console.WriteLine();

            var tiempoCosecha = PedirDatoObligatorio("Ingrese el tiempo de cosecha: ");
            var maduracion = PedirDatoObligatorio("Ingrese la maduración: ");
            var nutricion = PedirDatoObligatorio("Ingrese la nutrición: ");
            var densidadSiembra = PedirDatoObligatorio("Ingrese la densidad de siembra: ");
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

            var nuevoGrupoGenetico = new Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities.GrupoGenetico { nombre_grupo = grupoGenetico };
            var nuevoPorte = new Cafe_Colombiano.src.Modules.Porte.Domain.Entities.Porte { nombre_porte = porte };
            var nuevoTamanoGrano = new Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities.TamanoGrano { nombre_tamano = tamanoGrano };
            var nuevaAltitudOptima = new Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities.AltitudOptima { rango_altitud = altitudOptima, descripcion = descripcionAltitudOptima };
            var nuevoPotencialRendimiento = new Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities.PotencialRendimiento { nivel_rendimiento = potencialRendimiento };
            var nuevaCalidadGrano = new Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities.CalidadGrano { nivel_calidad = calidadGrano };
            var nuevoTipoResistencia = new Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities.TipoResistencia { nombre_tipo = tipoResistencia };
            var nuevoNivelResistencia = new Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities.NivelResistencia { nombre_nivel = nivelResistencia };

            var nuevaInformacionAgronomica = new Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities.InformacionAgronomica
            {
                tiempo_cosecha = tiempoCosecha,
                maduracion = maduracion,
                nutricion = nutricion,
                densidad_siembra = densidadSiembra
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