using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class VariedadShowCatalogoService : IVariedadMenuController
    {
        private readonly IVariedadService _variedadService;

        public VariedadShowCatalogoService(IVariedadService variedadService)
        {
            _variedadService = variedadService;
        }
        public async Task HandleShowCatalogo()
        {
            var variedades = await _variedadService.ConsultarCatalogoAsync(); 

            if (variedades.Any())
            {
                var catalogo = await _variedadService.ConsultarCatalogoAsync();
                foreach (var u in catalogo)
                {
                    Console.WriteLine("".PadRight(80, '='));
                    Console.WriteLine($"ID: {u.id}, Nombre: {u.nombre_comun} ({u.nombre_cientifico})\n");
                    Console.WriteLine($"{u.imagen_referencia_url}\n");
                    Console.WriteLine($"Descripción: {u.descripcion_general}\n");

                }

            }
            else
            {
                Console.WriteLine("No hay variedades disponibles para mostrar.");
            }

        }
    }
}