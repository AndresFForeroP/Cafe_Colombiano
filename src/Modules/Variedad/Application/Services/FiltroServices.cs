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
            var Variedad = FiltrarPorResistencia(variedades);
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
               Filtrar Por Porte
        ==============================
        1.Alto.
        2.Bajo.
        3.Dwarf/Compact.
        4.Tall.
        5.Desconocido.
        9.Cancelar Filtro.
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
        private IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorTamano(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";
            Console.WriteLine("""
        ==============================
             Filtra Por Tamaño
        ==============================
        1.Pequeño.
        2.Mediano.
        3.Grande.
        4.Muy grande.
        5.Desconocido.
        9.Cancelar Filtro.
        ==============================
        """);
            Console.WriteLine("Ingrese un numero segun el Tamaño del grano que desea buscar");
            int porte = validarentero(5);
            switch (porte)
            {
                case 1:
                    nombre = "Pequeño";
                    break;
                case 2:
                    nombre = "Mediano";
                    break;
                case 3:
                    nombre = "Grande";
                    break;
                case 4:
                    nombre = "Muy Grande";
                    break;
                case 5:
                    nombre = "Desconocido";
                    break;
            }
            return Lista.Where(v => v.TamanoGrano?.nombre_tamano != null && v.TamanoGrano.nombre_tamano.ToLower().Contains(nombre.ToLower()));
        }
        private IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorAltitud(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";
            Console.WriteLine("""
        ==============================
             Filtra Por Altitud
        ==============================
        1.500-1000 msnm.
        2.400-900 msnm.
        3.500-800 msnm.
        4.700 msnm.
        5.1200-1800 msnm.
        9.Cancelar Filtro
        ==============================
        """);
            Console.WriteLine("Ingrese un numero segun la Altitud Optima del grano que desea buscar");
            int porte = validarentero(5);
            switch (porte)
            {
                case 1:
                    nombre = "500-1000 msnm";
                    break;
                case 2:
                    nombre = "400-900 msnm";
                    break;
                case 3:
                    nombre = "500-800 msnm";
                    break;
                case 4:
                    nombre = "700 msnm";
                    break;
                case 5:
                    nombre = "1200-1800 msnm";
                    break;
            }
            return Lista.Where(v => v.AltitudOptima?.rango_altitud != null && v.AltitudOptima.rango_altitud.ToLower().Contains(nombre.ToLower()));
        }
        private IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorRendimiento(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";
            Console.WriteLine("""
        ==============================
            Filtra Por Rendimiento
        ==============================
        1.Bajo (menos de 1500 kg/ha).
        2.Medio (1500-3000 kg/ha).
        3.Alto (3000-5000 kg/ha).
        4.Muy alto (más de 5000 kg/ha).
        5.Desconocido.
        9.Cancelar Filtro
        ==============================
        """);
            Console.WriteLine("Ingrese un numero segun el Potencial de Rendimiento del grano que desea buscar");
            int porte = validarentero(5);
            switch (porte)
            {
                case 1:
                    nombre = "Bajo (menos de 1500 kg/ha)";
                    break;
                case 2:
                    nombre = "Medio (1500-3000 kg/ha)";
                    break;
                case 3:
                    nombre = "Alto (3000-5000 kg/ha)";
                    break;
                case 4:
                    nombre = "Muy alto (más de 5000 kg/ha)";
                    break;
                case 5:
                    nombre = "Desconocido";
                    break;
            }
            return Lista.Where(v => v.PotencialRendimiento?.nivel_rendimiento!= null && v.PotencialRendimiento.nivel_rendimiento.ToLower().Contains(nombre.ToLower()));
        }
        private IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorCalidad(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";
            Console.WriteLine("""
        ==============================
              Filtra Por Calidad
        ==============================
        1.Excelente.
        2.Muy buena.
        3.Buena.
        4.Regular.
        5.Básica.
        6.Desconocida
        9.Cancelar Filtro
        ==============================
        """);
            Console.WriteLine("Ingrese un numero segun la Calidad del grano que desea buscar");
            int porte = validarentero(6);
            switch (porte)
            {
                case 1:
                    nombre = "Excelente";
                    break;
                case 2:
                    nombre = "Muy buena";
                    break;
                case 3:
                    nombre = "Buena";
                    break;
                case 4:
                    nombre = "Regular";
                    break;
                case 5:
                    nombre = "Básica";
                    break;
                case 6:
                    nombre = "Desconocida";
                    break;
            }
            return Lista.Where(v => v.CalidadGrano?.nivel_calidad!= null && v.CalidadGrano.nivel_calidad.ToLower().Contains(nombre.ToLower()));
        }
        private IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorResistencia(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";
            Console.WriteLine("""
        ==============================
            Filtra Por Resistencias
        ==============================
        1.Roya del cafeto.
        2.Enfermedad de la cereza del café (CBD).
        3.Nematodos.
        4.Broca del café.
        5.Barrenador del tallo (Xylosandus compactus).
        6.Marchitez del café (CWD)
        7.Antracnosis
        8.Enfermedad de la ampolla roja
        9.Cancelar Filtro
        ==============================
        """);
            Console.WriteLine("Ingrese un numero segun la Resistencia del grano que desea buscar");
            int porte = validarentero(8);
            switch (porte)
            {
                case 1:
                    nombre = "Roya del cafeto";
                    break;
                case 2:
                    nombre = "Enfermedad de la cereza del café (CBD)";
                    break;
                case 3:
                    nombre = "Nematodos";
                    break;
                case 4:
                    nombre = "Broca del café";
                    break;
                case 5:
                    nombre = "Barrenador del tallo (Xylosandus compactus)";
                    break;
                case 6:
                    nombre = "Marchitez del café (CWD)";
                    break;
                case 7:
                    nombre = "Antracnosis";
                    break;
                case 8:
                    nombre = "Enfermedad de la ampolla roja";
                    break;
            }
            string NivelResistencia = "";
            Console.WriteLine("""
        ==============================
            Nivel de Resistencias
        ==============================
        1.Resistente.
        2.Tolerante.
        3.Susceptible.
        4.Desconocido.
        9.Cancelar Filtro
        ==============================
        """);
            Console.WriteLine("Ingrese un numero segun el nivel de resitencia que desea buscar");
            int NivelRes = validarentero(4);
            switch (NivelRes)
            {
                case 1:
                    NivelResistencia = "Resistente";
                    break;
                case 2:
                    NivelResistencia = "Tolerante";
                    break;
                case 3:
                    NivelResistencia = "Susceptible";
                    break;
                case 4:
                    NivelResistencia = "Desconocido";
                    break;
            }
            return Lista.Where(v => v.VariedadesResistencia != null && v.VariedadesResistencia.Any(r =>
        !string.IsNullOrEmpty(r.TipoResistencia?.nombre_tipo) &&
        r.TipoResistencia.nombre_tipo.Contains(nombre, StringComparison.OrdinalIgnoreCase) &&
        !string.IsNullOrEmpty(r.NivelResistencia?.nombre_nivel) &&
        r.NivelResistencia.nombre_nivel.Contains(NivelResistencia, StringComparison.OrdinalIgnoreCase)));
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