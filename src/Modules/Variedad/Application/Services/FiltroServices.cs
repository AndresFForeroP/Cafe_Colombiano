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
            var Variedad = FiltrarPorPorte(variedades);
            foreach (var v in Variedad)
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
        }
        private IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorNombre(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            Console.WriteLine("Ingrese el nombre cientifico del grano que desea buscar");
            string nombre = Console.ReadLine() ?? "";
            return Lista.Where(v => v.nombre_cientifico != null && v.nombre_cientifico.ToLower().Contains(nombre.ToLower()));
        }
        private IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorPorte(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";
            Console.WriteLine("""
        ==============================
            Explorar Productos
        ==============================
        1.Alto.
        2.Bajo.
        3.Dwarf/Compact
        4.Tall
        5.Desconocido
        9.Cancelar Filtro
        ==============================
        """);
            Console.WriteLine("Ingrese un numero segun el Porte del grano que desea buscar");
            int porte = validarentero(5);
            switch (porte)
            {
                case 1:
                    nombre = "Alto";
                    break;
                case 2:
                    nombre = "Bajo";
                    break;
                case 3:
                    nombre = "Dwarf/Compact";
                    break;
                case 4:
                    nombre = "Tall";
                    break;
                case 5:
                    nombre = "Desconocido";
                    break;
            }
            return Lista.Where(v => v.Porte?.nombre_porte != null && v.Porte.nombre_porte.ToLower().Contains(nombre.ToLower()));
        }
        private int validarentero(int maximo)
        {
            int salida = 0;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out salida) || (salida < 1 || salida > maximo) && salida != 9)
                {
                    Console.WriteLine("VALOR INGRESADO NO VALIDO");
                }
            }
            while (salida < 1 || salida > maximo);
            return salida;
        }
    }
}