using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Application.Services;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository
{
    public class VariedadRepository : IVariedadRepository
    {
        internal readonly DbContext _context;

        public VariedadRepository(DbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>> GetAllVariedadesAsync()
        {
            return await _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>()
                .Include(v => v.AltitudOptima)
                .Include(v => v.GrupoGenetico)
                .Include(v => v.Porte)
                .Include(v => v.TamanoGrano)
                .Include(v => v.PotencialRendimiento)
                .Include(v => v.CalidadGrano)
                .Include(v => v.InformacionAgronomica)
                .Include(v => v.VariedadesResistencia!)
                    .ThenInclude(vr => vr.NivelResistencia)
                .Include(v => v.VariedadesResistencia!)
                    .ThenInclude(vr => vr.TipoResistencia)
                .ToListAsync();
        }
        public async Task<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad?> GetVariedadByIdAsync(int id)
        {
            return await _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>()
                .Include(v => v.AltitudOptima)
                .Include(v => v.GrupoGenetico)
                .Include(v => v.Porte)
                .Include(v => v.TamanoGrano)
                .Include(v => v.PotencialRendimiento)
                .Include(v => v.CalidadGrano)
                .Include(v => v.InformacionAgronomica)
                .Include(v => v.VariedadesResistencia!)
                    .ThenInclude(vr => vr.NivelResistencia)
                .Include(v => v.VariedadesResistencia!)
                    .ThenInclude(vr => vr.TipoResistencia)
                .FirstOrDefaultAsync(v => v.id == id);
        }
        public async Task Add(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity)
        {
            await _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().AddAsync(entity);
        }
        public async Task Remove(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity)
        {
            _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public void RemoveEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task Update(Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad entity)
        {
            _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public Task ActualizarVariedadAsync(Domain.Entities.Variedad variedad)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorNombre(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold yellow]Ingrese el nombre científico del grano que desea buscar:[/]")
                    .PromptStyle("green")
                    .AllowEmpty()
            );

            return Lista.Where(v => v.nombre_cientifico != null &&
                                    v.nombre_cientifico.ToLower().Contains(nombre.ToLower()));
        }

        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorPorte(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Porte[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan2, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Alto",
                        "2. Bajo",
                        "3. Dwarf/Compact",
                        "4. Tall",
                        "5. Desconocido",
                        "9. Cancelar Filtro"
                    }));

            int porte = opcion.StartsWith("1") ? 1 :
                        opcion.StartsWith("2") ? 2 :
                        opcion.StartsWith("3") ? 3 :
                        opcion.StartsWith("4") ? 4 :
                        opcion.StartsWith("5") ? 5 : 9;

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

            return Lista.Where(v => v.Porte?.nombre_porte != null &&
                                    v.Porte.nombre_porte.ToLower().Contains(nombre.ToLower()));
        }

        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorTamano(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Tamaño[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Pequeño",
                        "2. Mediano",
                        "3. Grande",
                        "4. Muy Grande",
                        "5. Desconocido",
                        "9. Cancelar Filtro"
                    }));

            int tamano = opcion.StartsWith("1") ? 1 :
                        opcion.StartsWith("2") ? 2 :
                        opcion.StartsWith("3") ? 3 :
                        opcion.StartsWith("4") ? 4 :
                        opcion.StartsWith("5") ? 5 : 9;

            switch (tamano)
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

            return Lista.Where(v => v.TamanoGrano?.nombre_tamano != null &&
                                    v.TamanoGrano.nombre_tamano.ToLower().Contains(nombre.ToLower()));
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorAltitud( IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Altitud[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. 500-1000 msnm",
                        "2. 400-900 msnm",
                        "3. 500-800 msnm",
                        "4. 700 msnm",
                        "5. 1200-1800 msnm",
                        "9. Cancelar Filtro"
                    }));

            int altitud = opcion.StartsWith("1") ? 1 :
                        opcion.StartsWith("2") ? 2 :
                        opcion.StartsWith("3") ? 3 :
                        opcion.StartsWith("4") ? 4 :
                        opcion.StartsWith("5") ? 5 : 9;

            switch (altitud)
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

            return Lista.Where(v => v.AltitudOptima?.rango_altitud != null &&
                                    v.AltitudOptima.rango_altitud.ToLower().Contains(nombre.ToLower()));
        }

       public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorRendimiento(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Rendimiento[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Bajo (menos de 1500 kg/ha)",
                        "2. Medio (1500-3000 kg/ha)",
                        "3. Alto (3000-5000 kg/ha)",
                        "4. Muy alto (más de 5000 kg/ha)",
                        "5. Desconocido",
                        "9. Cancelar Filtro"
                    }));

            int rendimiento = opcion.StartsWith("1") ? 1 :
                            opcion.StartsWith("2") ? 2 :
                            opcion.StartsWith("3") ? 3 :
                            opcion.StartsWith("4") ? 4 :
                            opcion.StartsWith("5") ? 5 : 9;

            switch (rendimiento)
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

            return Lista.Where(v => v.PotencialRendimiento?.nivel_rendimiento != null &&
                                    v.PotencialRendimiento.nivel_rendimiento.ToLower().Contains(nombre.ToLower()));
        }

        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorCalidad(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Calidad[/]")
                    .PageSize(8)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Excelente",
                        "2. Muy buena",
                        "3. Buena",
                        "4. Regular",
                        "5. Básica",
                        "6. Desconocida",
                        "9. Cancelar Filtro"
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

            return Lista.Where(v => v.CalidadGrano?.nivel_calidad != null &&
                                    v.CalidadGrano.nivel_calidad.ToLower().Contains(nombre.ToLower()));
        }

       public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorResistencia(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";
            string NivelResistencia = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Resistencias[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
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
                        "9. Cancelar Filtro"
                    }));

            int tipo = opcion.StartsWith("1") ? 1 :
                    opcion.StartsWith("2") ? 2 :
                    opcion.StartsWith("3") ? 3 :
                    opcion.StartsWith("4") ? 4 :
                    opcion.StartsWith("5") ? 5 :
                    opcion.StartsWith("6") ? 6 :
                    opcion.StartsWith("7") ? 7 :
                    opcion.StartsWith("8") ? 8 : 9;

            switch (tipo)
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

            var opcionNivel = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Nivel de Resistencia[/]")
                    .PageSize(6)
                    .HighlightStyle(new Style(foreground: Color.Green, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Resistente",
                        "2. Tolerante",
                        "3. Susceptible",
                        "4. Desconocido",
                        "9. Cancelar Filtro"
                    }));

            int nivel = opcionNivel.StartsWith("1") ? 1 :
                        opcionNivel.StartsWith("2") ? 2 :
                        opcionNivel.StartsWith("3") ? 3 :
                        opcionNivel.StartsWith("4") ? 4 : 9;

            switch (nivel)
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

        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorTiempoCoshecha(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Tiempo de Cosecha[/]")
                    .PageSize(6)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. 6-8 meses",
                        "2. Año 2",
                        "3. Año 4",
                        "4. Desconocida",
                        "9. Cancelar Filtro"
                    }));

            int tiempo = opcion.StartsWith("1") ? 1 :
                        opcion.StartsWith("2") ? 2 :
                        opcion.StartsWith("3") ? 3 :
                        opcion.StartsWith("4") ? 4 : 9;

            switch (tiempo)
            {
                case 1:
                    nombre = "Principal: 6-8 meses después de floración";
                    break;
                case 2:
                    nombre = "Año 2";
                    break;
                case 3:
                    nombre = "Año 4";
                    break;
                case 4:
                    nombre = "Desconocida";
                    break;
            }

            return Lista.Where(v => v.InformacionAgronomica?.tiempo_cosecha != null &&
                                    v.InformacionAgronomica.tiempo_cosecha.ToLower().Contains(nombre.ToLower()));
        }

       public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorMaduracion(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Maduración[/]")
                    .PageSize(5)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Promedio",
                        "2. Tardía",
                        "9. Cancelar Filtro"
                    }));

            int maduracion = opcion.StartsWith("1") ? 1 :
                            opcion.StartsWith("2") ? 2 : 9;

            switch (maduracion)
            {
                case 1:
                    nombre = "Promedio";
                    break;
                case 2:
                    nombre = "Tardía";
                    break;
            }

            return Lista.Where(v => v.InformacionAgronomica?.maduracion != null &&
                                    v.InformacionAgronomica.maduracion.ToLower().Contains(nombre.ToLower()));
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorNutricion(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Nutrición[/]")
                    .PageSize(5)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Media",
                        "2. Alta",
                        "3. Desconocida",
                        "9. Cancelar Filtro"
                    }));

            int nutricion = opcion.StartsWith("1") ? 1 :
                            opcion.StartsWith("2") ? 2 :
                            opcion.StartsWith("3") ? 3 : 9;

            switch (nutricion)
            {
                case 1:
                    nombre = "Media";
                    break;
                case 2:
                    nombre = "Alta";
                    break;
                case 3:
                    nombre = "Desconocida";
                    break;
            }

            return Lista.Where(v => v.InformacionAgronomica?.nutricion != null &&
                                    v.InformacionAgronomica.nutricion.ToLower().Contains(nombre.ToLower()));
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorDensidad(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Densidad de Siembra[/]")
                    .PageSize(7)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. 1000-2000 plantas/ha (usando poda de un solo tallo)",
                        "2. 2,500 árboles/ha",
                        "3. hasta 3,000 plantas/ha",
                        "4. 2000-3000 plantas/ha (usando poda de múltiples tallos)",
                        "5. hasta 10,000 cafetos/ha",
                        "9. Cancelar Filtro"
                    }));

            int densidad = opcion.StartsWith("1") ? 1 :
                        opcion.StartsWith("2") ? 2 :
                        opcion.StartsWith("3") ? 3 :
                        opcion.StartsWith("4") ? 4 :
                        opcion.StartsWith("5") ? 5 : 9;

            switch (densidad)
            {
                case 1:
                    nombre = "1000-2000 plantas/ha (usando poda de un solo tallo)";
                    break;
                case 2:
                    nombre = "2,500 árboles/ha";
                    break;
                case 3:
                    nombre = "hasta 3,000 plantas/ha";
                    break;
                case 4:
                    nombre = "2000-3000 plantas/ha (usando poda de múltiples tallos)";
                    break;
                case 5:
                    nombre = "hasta 10,000 cafetos/ha";
                    break;
            }

            return Lista.Where(v => v.InformacionAgronomica?.densidad_siembra != null &&
                                    v.InformacionAgronomica.densidad_siembra.ToLower().Contains(nombre.ToLower()));
        }
        public IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> FiltrarPorGrupo(IEnumerable<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad> Lista)
        {
            string nombre = "";

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Filtrar Por Grupo Genético[/]")
                    .PageSize(8)
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices(new[]
                    {
                        "1. Arábigo",
                        "2. Guinea",
                        "3. Congo",
                        "4. Uganda",
                        "5. Guinea x Congo",
                        "6. Guinea x Coffea congensis",
                        "9. Cancelar Filtro"
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
                    nombre = "Arábigo";
                    break;
                case 2:
                    nombre = "Guinea";
                    break;
                case 3:
                    nombre = "Congo";
                    break;
                case 4:
                    nombre = "Uganda";
                    break;
                case 5:
                    nombre = "Guinea x Congo";
                    break;
                case 6:
                    nombre = "Guinea x Coffea congensis";
                    break;
            }

            return Lista.Where(v => v.GrupoGenetico?.nombre_grupo != null &&
                                    v.GrupoGenetico.nombre_grupo.ToLower().Contains(nombre.ToLower()));
        }

        
        public int validarentero(int maximo)
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
        public string MostrasListaIds()
        {
            var variedades = _context.Set<Cafe_Colombiano.src.Modules.Variedad.Domain.Entities.Variedad>().ToList();

            foreach (var variedad in variedades)
            {
                Console.WriteLine($"ID: {variedad.id}, Nombre: {variedad.nombre_comun} ({variedad.nombre_cientifico})");
            }

            return "Lista de IDs mostrada en consola.";
        }

    }
}