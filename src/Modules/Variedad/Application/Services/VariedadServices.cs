using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class VariedadServices : IVaredadServices
    {
        public void MostrarVariedades(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> variedad)
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
        public string Validarfiltros()
        {
            Console.Clear();
            Console.WriteLine("Desea agregar otro filtro a la busqueda? (si/no)");
            string Respuesta = Console.ReadLine() ?? "";
            while (Respuesta.ToLower() != "no" && Respuesta.ToLower() != "si")
            {
                Console.WriteLine("Valor invalido");
                Console.WriteLine("Ingrese si para agregar otro filtro o no si desea ver las variedades con los filtros aplicados");
                Respuesta = Console.ReadLine() ?? "";
            }
            return Respuesta.ToLower();
        }
    }
}