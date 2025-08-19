using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Cafe_Colombiano.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRepositoy
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cafe_Colombiano.src.Modules.Usuario.Domain.Entities.Usuario>> GetUserAsync()
        {
            return await _context.Usuario.ToListAsync();
        }
    }
}