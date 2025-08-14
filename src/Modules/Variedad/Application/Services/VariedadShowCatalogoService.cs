using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class VariedadShowCatalogoService 
    {
        private readonly VariedadRepository _repo;

        public VariedadShowCatalogoService(VariedadRepository repo)
        {
            _repo = repo;
        }
        public async Task ShowCatalogo()
        {
            var variedades = await _repo.ConsultarCatalogoAsync();

            if (variedades.Any())
            {
                foreach (var u in variedades)
                {
                    Console.WriteLine("".PadRight(80, '='));
                    Console.WriteLine($"ID: {u.id}, Nombre: {u.nombre_comun} ({u.nombre_cientifico})\n");

                    Console.WriteLine($"Imagen: {u.imagen_referencia_url}\n");
                    Console.WriteLine($"Descripción: {u.descripcion_general}\n");
                    Console.WriteLine($"Historia del linaje: {u.historia_linaje}\n");
                    Console.WriteLine($"Grupo Genético: {u.GrupoGenetico?.nombre_grupo}\n");
                    Console.WriteLine($"Porte: {u.Porte?.nombre_porte}\n");
                    Console.WriteLine($"Tamaño de Grano: {u.TamanoGrano?.nombre_tamano}\n");
                    Console.WriteLine($"Altitud Óptima: {u.AltitudOptima?.descripcion}\n");
                    Console.WriteLine($"Potencial de Rendimiento: {u.PotencialRendimiento?.nivel_rendimiento}\n");
                    Console.WriteLine($"Calidad de Grano: {u.CalidadGrano?.nivel_calidad}\n");
                    Console.WriteLine($"Información Agronómica: {u.InformacionAgronomica?.densidad_siembra}\n");
                }

            }
            else
            {
                Console.WriteLine("No hay variedades disponibles para mostrar.");

            }

        }

    }
}

