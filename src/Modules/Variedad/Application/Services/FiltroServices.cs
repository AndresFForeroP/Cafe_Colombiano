using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;

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
            Console.Clear();
            var variedades = await _repo.GetAllVariedadesAsync();

            foreach (var v in variedades)
            {
                Console.WriteLine($"--- Variedad ---");
                Console.WriteLine($"ID: {v.id}");
                Console.WriteLine($"Nombre: {v.nombre_cientifico}");
                Console.WriteLine($"Altitud Óptima: {v.AltitudOptima?.rango_altitud} - {v.AltitudOptima?.descripcion}");
                Console.WriteLine($"Grupo Genético: {v.GrupoGenetico?.nombre_grupo} - {v.GrupoGenetico?.origen}");
                Console.WriteLine($"Porte: {v.Porte?.nombre_porte}");
                Console.WriteLine($"Tamaño Grano: {v.TamanoGrano?.nombre_tamano}");
                Console.WriteLine($"Potencial Rendimiento: {v.PotencialRendimiento?.nivel_rendimiento}");
                Console.WriteLine($"Calidad Grano: {v.CalidadGrano?.nivel_calidad}");
                Console.WriteLine($"Información Agronómica: {v.InformacionAgronomica?.maduracion}");

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
            Console.WriteLine("Fin del listado");
            Console.ReadKey();
        }
    }
}