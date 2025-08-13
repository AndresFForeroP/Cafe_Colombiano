using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cafe_Colombiano.src.Modules.Usuario.Application.Interfaces;
using Liga_futbol.Src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Colombiano.src.Modules.Usuario.Infrastructure.Repository
{
    public class ExplorarVrepository : IExplorarVrepository
    {
        private readonly AppDbContext _context;
        public ExplorarVrepository(AppDbContext context)
        {
            _context = context;
        }
    }
}