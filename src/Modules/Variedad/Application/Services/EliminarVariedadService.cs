using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class EliminarVariedadService : IEliminarVariedadService
    {
        private readonly IVariedadRepository _repo;

        public EliminarVariedadService(IVariedadRepository repo)
        {
            _repo = repo;

        }


        public async Task EliminarVariedadAsync()
        {
            while (true)
            {
                _repo.MostrasListaIds();

                Console.WriteLine("Seleccione el ID de la variedad a eliminar:");
                if (int.TryParse(Console.ReadLine(), out int idVariedad))
                {
                    var idEliminar = await _repo.GetVariedadByIdAsync(idVariedad);

                    if (idEliminar != null)
                    {
                        Console.Clear();
                        await _repo.Remove(idEliminar);
                        Console.WriteLine("Variedad y todos sus datos relacionados eliminados con éxito.");
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("No se encontró la variedad con ese ID.");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("ID inválido.");
                }
            }
        }

    }
}