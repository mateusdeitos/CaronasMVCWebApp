using CaronasMVCWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Services
{
    public class DestinyService
    {
        private readonly caronas_app_dbContext _context;

        public DestinyService(caronas_app_dbContext context)
        {
            _context = context;
        }
        public async Task<List<Destiny>> FindAllAsync()
        {
            return await _context.Destiny.OrderBy(x => x.Name).ToListAsync();
        }

        internal async Task<Destiny> FindByIdAsync(int destinyId)
        {
            return await _context.Destiny.FirstOrDefaultAsync(x => x.Id == destinyId);
        }

    }
}
