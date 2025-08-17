using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;

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
                        variedadToUpdate.GrupoGenetico.nombre_grupo = PedirDatoOpcional("Ingrese el nuevo grupo genético de la variedad:", variedadToUpdate.GrupoGenetico.nombre_grupo);
                        variedadToUpdate.GrupoGenetico.origen = PedirDatoOpcional("Ingrese el nuevo origen de la variedad:", variedadToUpdate.GrupoGenetico.origen);
                    }

                    // Porte
                    if (variedadToUpdate.Porte != null)
                    {
                        variedadToUpdate.Porte.nombre_porte = PedirDatoOpcional("Ingrese el nuevo porte de la planta:", variedadToUpdate.Porte.nombre_porte);
                    }

                    // Tamaño de Grano
                    if (variedadToUpdate.TamanoGrano != null)
                    {
                        variedadToUpdate.TamanoGrano.nombre_tamano = PedirDatoOpcional("Ingrese el nuevo tamaño de grano:", variedadToUpdate.TamanoGrano.nombre_tamano);
                    }

                    // Altitud Óptima
                    if (variedadToUpdate.AltitudOptima != null)
                    {
                        variedadToUpdate.AltitudOptima.rango_altitud = PedirDatoOpcional("Ingrese el nuevo rango de altitud óptima:", variedadToUpdate.AltitudOptima.rango_altitud);
                        variedadToUpdate.AltitudOptima.descripcion = PedirDatoOpcional("Ingrese la nueva descripción de la altitud óptima:", variedadToUpdate.AltitudOptima.descripcion);
                    }

                    // Potencial de Rendimiento
                    if (variedadToUpdate.PotencialRendimiento != null)
                    {
                        variedadToUpdate.PotencialRendimiento.nivel_rendimiento = PedirDatoOpcional("Ingrese el nuevo potencial de rendimiento:", variedadToUpdate.PotencialRendimiento.nivel_rendimiento);
                    }

                    // Calidad de Grano
                    if (variedadToUpdate.CalidadGrano != null)
                    {
                        variedadToUpdate.CalidadGrano.nivel_calidad = PedirDatoOpcional("Ingrese la nueva calidad de grano:", variedadToUpdate.CalidadGrano.nivel_calidad);
                        variedadToUpdate.CalidadGrano.descripcion = PedirDatoOpcional("Ingrese la nueva descripción de la calidad de grano:", variedadToUpdate.CalidadGrano.descripcion);
                    }

                    // Información Agronómica
                    if (variedadToUpdate.InformacionAgronomica != null)
                    {
                        variedadToUpdate.InformacionAgronomica.tiempo_cosecha = PedirDatoOpcional("Ingrese el nuevo tiempo de cosecha:", variedadToUpdate.InformacionAgronomica.tiempo_cosecha);
                        variedadToUpdate.InformacionAgronomica.maduracion = PedirDatoOpcional("Ingrese la nueva maduración:", variedadToUpdate.InformacionAgronomica.maduracion);
                        variedadToUpdate.InformacionAgronomica.nutricion = PedirDatoOpcional("Ingrese la nueva nutrición:", variedadToUpdate.InformacionAgronomica.nutricion);
                        variedadToUpdate.InformacionAgronomica.densidad_siembra = PedirDatoOpcional("Ingrese la nueva densidad de siembra:", variedadToUpdate.InformacionAgronomica.densidad_siembra);
                    }

                    // VariedadesResistencia (solo el primero, puedes adaptar para varios)
                    var resistencia = variedadToUpdate.VariedadesResistencia?.FirstOrDefault();
                    if (resistencia != null)
                    {
                        if (resistencia.TipoResistencia != null)
                        {
                            resistencia.TipoResistencia.nombre_tipo = PedirDatoOpcional("Ingrese el nuevo tipo de resistencia:", resistencia.TipoResistencia.nombre_tipo);
                        }
                        if (resistencia.NivelResistencia != null)
                        {
                            resistencia.NivelResistencia.nombre_nivel = PedirDatoOpcional("Ingrese el nuevo nivel de resistencia:", resistencia.NivelResistencia.nombre_nivel);
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