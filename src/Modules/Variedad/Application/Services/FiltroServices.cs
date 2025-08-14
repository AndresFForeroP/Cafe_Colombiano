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
            List<string> FiltrosAplicados= [];
            Console.Clear();
            string Respuesta = "";
            var variedades = await _repo.GetAllVariedadesAsync();
            var Variedad = _repo.FiltrarPorGrupo(variedades);
            do
            {
                ImprimirMenuFiltros(FiltrosAplicados);
            } while (Respuesta != "no");
            MostrarVariedades(Variedad);
        }
        private int validarentero()
        {
            // falta implementar validaciones de enteros
            return 1;
        }
        private void ImprimirMenuFiltros(List<string> filtros)
        {
            bool hayFiltrosDisponibles = false;
            Console.WriteLine("""
        ==============================
            Filtros Disponibles
        ==============================
        """);
            if (!filtros.Contains("nombre"))
            {
                Console.WriteLine("1. Filtrar por Nombre de grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("porte"))
            {
                Console.WriteLine("2. Filtrar por Porte del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("tamano"))
            {
                Console.WriteLine("3. Filtrar por Tamaño del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("altitud"))
            {
                Console.WriteLine("4. Filtrar por Altitud del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("rendimiento"))
            {
                Console.WriteLine("5. Filtrar por Rendimiento del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("calidad"))
            {
                Console.WriteLine("6. Filtrar por Calidad del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("resistencia"))
            {
                Console.WriteLine("7. Filtrar por Resistencia del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("tiempocosecha"))
            {
                Console.WriteLine("8. Filtrar por Tiempo de cosecha del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("maduracion"))
            {
                Console.WriteLine("9. Filtrar por Maduración del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("nutricion"))
            {
                Console.WriteLine("10. Filtrar por Nutrición del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("densidad"))
            {
                Console.WriteLine("11. Filtrar por Densidad del grano");
                hayFiltrosDisponibles = true;
            }
            if (!filtros.Contains("grupo"))
            {
                Console.WriteLine("12. Filtrar por Grupo del grano");
                hayFiltrosDisponibles = true;
            }

            if (!hayFiltrosDisponibles)
            {
                Console.WriteLine("Ya has aplicado todos los filtros");
            }
            Console.WriteLine("==============================");
            Console.WriteLine("Ingrese un número según el filtro que desea aplicar");
        }

        private void MostrarVariedades(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> variedad)
        {
            foreach (var v in variedad)
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
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        } 
        
    }
}