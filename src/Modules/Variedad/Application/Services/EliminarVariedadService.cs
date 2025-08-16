using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Variedad.Infrastructure.Repository;
using Cafe_Colombiano.src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Colombiano.src.Modules.Variedad.Application.Services
{
    public class EliminarVariedadService
    {
        private readonly VariedadRepository _repo;

        public EliminarVariedadService(VariedadRepository repo)
        {
            _repo = repo;

        }


        public async Task EliminarVariedadAsync()
        {
            while (true)
            {

                var verCatalogos = await _repo.GetAllVariedadesAsync();
                foreach (var variedad in verCatalogos)
                {
                    Console.WriteLine($"ID: {variedad.id}, Nombre: {variedad.nombre_comun} ({variedad.nombre_cientifico})");
                }

                Console.WriteLine("Seleccione el ID de la variedad a eliminar:");
                if (int.TryParse(Console.ReadLine(), out int idVariedad))
                {
                    // Carga la variedad con todas sus relaciones
                    var idEliminar = await _repo.GetVariedadByIdAsync(idVariedad);

                    if (idEliminar != null)
                    {
                        Console.Clear();

                        // Elimina resistencias asociadas
                        if (idEliminar.VariedadesResistencia != null)
                        {
                            foreach (var resistencia in idEliminar.VariedadesResistencia.ToList())
                            {
                                _repo._context.Set<Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities.VariedadResistencia>().Remove(resistencia);
                            }
                        }

                        // Elimina entidades relacionadas si no hay cascada
                        if (idEliminar.GrupoGenetico != null)
                            _repo._context.Set<Cafe_Colombiano.src.Modules.GrupoGenetico.Domain.Entities.GrupoGenetico>().Remove(idEliminar.GrupoGenetico);

                        if (idEliminar.Porte != null)
                            _repo._context.Set<Cafe_Colombiano.src.Modules.Porte.Domain.Entities.Porte>().Remove(idEliminar.Porte);

                        if (idEliminar.TamanoGrano != null)
                            _repo._context.Set<Cafe_Colombiano.src.Modules.TamanoGrano.Domain.Entities.TamanoGrano>().Remove(idEliminar.TamanoGrano);

                        if (idEliminar.AltitudOptima != null)
                            _repo._context.Set<Cafe_Colombiano.src.Modules.AltitudOptima.Domain.Entities.AltitudOptima>().Remove(idEliminar.AltitudOptima);

                        if (idEliminar.PotencialRendimiento != null)
                            _repo._context.Set<Cafe_Colombiano.src.Modules.PotencialRendimiento.Domain.Entities.PotencialRendimiento>().Remove(idEliminar.PotencialRendimiento);

                        

                        if (idEliminar.InformacionAgronomica != null)
                            _repo._context.Set<Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities.InformacionAgronomica>().Remove(idEliminar.InformacionAgronomica);
                        if (idEliminar.VariedadesResistencia != null)
                        {
                            foreach (var resistencia in idEliminar.VariedadesResistencia)
                            {
                                     
                                if (resistencia.TipoResistencia != null)
                                    _repo._context.Set<Cafe_Colombiano.src.Modules.TipoResistencia.Domain.Entities.TipoResistencia>().Remove(resistencia.TipoResistencia);

                                if (resistencia.NivelResistencia != null)
                                    _repo._context.Set<Cafe_Colombiano.src.Modules.NivelResistencia.Domain.Entities.NivelResistencia>().Remove(resistencia.NivelResistencia);

                                    _repo._context.Set<Cafe_Colombiano.src.Modules.VariedadResistencia.Domain.Entities.VariedadResistencia>().Remove(resistencia);

                            }
                        }   
                  
                        if (idEliminar.Porte != null)
                            _repo._context.Set<Cafe_Colombiano.src.Modules.Porte.Domain.Entities.Porte>().Remove(idEliminar.Porte);

                        if (idEliminar.CalidadGrano != null)
                            _repo._context.Set<Cafe_Colombiano.src.Modules.CalidadGrano.Domain.Entities.CalidadGrano>().Remove(idEliminar.CalidadGrano);
                        if (idEliminar.InformacionAgronomica != null)
                            _repo._context.Set<Cafe_Colombiano.src.Modules.InformacionAgronomica.Domain.Entities.InformacionAgronomica>().Remove(idEliminar.InformacionAgronomica);

                        
                        
                        // Finalmente elimina la variedad principal
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