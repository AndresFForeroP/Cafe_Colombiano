using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;


namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class CrearVariedadService 
    {
        private readonly VariedadRepository _variedadService;

        public CrearVariedadService(VariedadRepository variedadService)
        {
            _variedadService = variedadService;
        }


        public async Task CrearVariedad()
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      ☕ CREAR NUEVA VARIEDAD DE CAFÉ ☕                            ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            Console.Write("Ingrese el nombre común de la variedad: ");
            string nombreComun;
            while (true)
            {
                nombreComun = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(nombreComun))
                {
                    Console.WriteLine("El nombre no puede estar vacío. Por favor, ingrese un nombre válido.");
                    continue;
                }

                var existe = await _variedadService.ConsultarCatalogoAsync();
                if (existe.Any(v => v.nombre_comun != null && v.nombre_comun.Equals(nombreComun, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("La variedad ya existe. Por favor, ingrese un nombre diferente.");
                }
                else
                {
                    break;
                }
            }


            Console.Write("Ingrese el nombre científico de la variedad: ");
            var nombreCientifico = Console.ReadLine();
            Console.Write("Ingrese la URL de la imagen de referencia: ");
            var imagenReferenciaUrl = Console.ReadLine();

            Console.Write("Ingrese la descripción general de la variedad: ");
            var descripcionGeneral = Console.ReadLine();

            Console.Write("Ingrese la historia del linaje de la variedad: ");
            var historiaLinaje = Console.ReadLine();

            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     GRUPO GENETICO                        ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            Console.Write("Ingrese el grupo genético de la variedad: ");
            var grupoGenetico = Console.ReadLine();

            Console.Write("Ingrese el origen de la variedad: ");
            var origen = Console.ReadLine();


            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              DETALLES DEL GRANO                ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.WriteLine();


            Console.Write("Ingrese el porte de la planta: ");
            var porte = Console.ReadLine();

            Console.Write("Ingrese el tamaño de grano: ");
            var tamanoGrano = Console.ReadLine();

            Console.Write("Ingrese la altitud óptima: ");
            var altitudOptima = Console.ReadLine();

            Console.Write("Ingrese el potencial de rendimiento: ");
            var potencialRendimiento = Console.ReadLine();

            Console.Write("Ingrese la calidad de grano: ");
            var calidadGrano = Console.ReadLine();
            Console.WriteLine("Descripcion de la calidad de grano: ");
            var descripcionCalidadGrano = Console.ReadLine();

            Console.Write("Tipo de resistencia: ");
            var tipoResistencia = Console.ReadLine();

            Console.Write("Nivel de resistencia: ");
            var nivelResistencia = Console.ReadLine();
            

            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      INFORMACION AGRONOMICA                            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            Console.Write("Ingrese el tiempo de cosecha: ");
            var tiempoCosecha = Console.ReadLine();
            Console.Write("Ingrese la maduración: ");
            var maduracion = Console.ReadLine();
            Console.Write("Ingrese la nutrición: ");
            var nutricion = Console.ReadLine();
            Console.Write("Ingrese la densidad de siembra: ");
            var densidadSiembra = Console.ReadLine();

           

            Console.Write("Ingrese el rango de Altitud: ");
            var rangoAltitud = Console.ReadLine();

            //NUEVA VARIEDAD
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
            var nuevaAltitudOptima = new Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities.AltitudOptima { descripcion = altitudOptima };
            var nuevoPotencialRendimiento = new Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities.PotencialRendimiento { nivel_rendimiento = potencialRendimiento };
            var nuevaCalidadGrano = new Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities.CalidadGrano { nivel_calidad = calidadGrano };
            var nuevoTipoResistencia = new Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities.TipoResistencia { nombre_tipo = tipoResistencia };
            var nuevoNivelResistencia = new Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities.NivelResistencia { nombre_nivel = nivelResistencia };
    
            // InformacionAgronomica
            var nuevaInformacionAgronomica = new Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities.InformacionAgronomica
            {
                tiempo_cosecha = tiempoCosecha,
                maduracion = maduracion,
                nutricion = nutricion,
                densidad_siembra = densidadSiembra
               
            };
            // grupoGenetico
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
  

            await _variedadService.CrearVariedadAsync(nuevaVariedad);

            Console.WriteLine("Variedad creada exitosamente.");
        }


    }
}