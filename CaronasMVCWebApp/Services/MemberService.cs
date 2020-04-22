using CaronasMVCWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Services
{
    public class MemberService
    {
        private readonly caronas_app_dbContext _context;

        public MemberService(caronas_app_dbContext context)
        {
            _context = context;
        }

        public async Task<List<Member>> FindAllAsync()
        {
            return await _context.Member.OrderBy(x => x.Name).ToListAsync();
        }

        internal async Task<Member> FindByIdAsync(int memberId)
        {
            return await _context.Member.FirstOrDefaultAsync(x => x.Id == memberId);
        }
    }
}
