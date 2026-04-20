using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Core.Interfaces;
using BlindMatchPAS.Infrastructure.Data;

namespace BlindMatchPAS.Infrastructure.Repositories
{
    public class SupervisorExpertiseRepository : ISupervisorExpertiseRepository
    {
        private readonly ApplicationDbContext _context;

        public SupervisorExpertiseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupervisorExpertise>> GetBySupervisorIdAsync(string supervisorId)
        {
            return await _context.SupervisorExpertises
                .Include(e => e.ResearchArea)
                .Where(e => e.SupervisorId == supervisorId)
                .ToListAsync();
        }

        public async Task AddAsync(SupervisorExpertise expertise)
        {
            await _context.SupervisorExpertises.AddAsync(expertise);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int expertiseId)
        {
            var expertise = await _context.SupervisorExpertises.FindAsync(expertiseId);
            if (expertise != null)
            {
                _context.SupervisorExpertises.Remove(expertise);
                await _context.SaveChangesAsync();
            }
        }
    }
}
