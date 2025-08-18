/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Application.Interfaces;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class EliminarVariedadService
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

                        // Elimina resistencias asociadas y sus tipos/niveles
                        if (idEliminar.VariedadesResistencia != null)
                        {
                            foreach (var resistencia in idEliminar.VariedadesResistencia.ToList())
                            {
                                if (resistencia.TipoResistencia != null)
                                    _repo.RemoveEntity(resistencia.TipoResistencia);

                                if (resistencia.NivelResistencia != null)
                                    _repo.RemoveEntity(resistencia.NivelResistencia);

                                _repo.RemoveEntity(resistencia);
                            }
                        }

                        // Elimina entidades relacionadas si no hay cascada
                        if (idEliminar.GrupoGenetico != null)
                            _repo.RemoveEntity(idEliminar.GrupoGenetico);

                        if (idEliminar.Porte != null)
                            _repo.RemoveEntity(idEliminar.Porte);

                        if (idEliminar.TamanoGrano != null)
                            _repo.RemoveEntity(idEliminar.TamanoGrano);

                        if (idEliminar.AltitudOptima != null)
                            _repo.RemoveEntity(idEliminar.AltitudOptima);

                        if (idEliminar.PotencialRendimiento != null)
                            _repo.RemoveEntity(idEliminar.PotencialRendimiento);

                        if (idEliminar.CalidadGrano != null)
                            _repo.RemoveEntity(idEliminar.CalidadGrano);

                        if (idEliminar.InformacionAgronomica != null)
                            _repo.RemoveEntity(idEliminar.InformacionAgronomica);

                        // Finalmente elimina la variedad principal
                        await _repo.Remove(idEliminar);

                        // Guarda todos los cambios
                        await _repo.SaveAsync();

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
} */