using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Core.Interfaces;
using BlindMatchPAS.Infrastructure.Data;

namespace BlindMatchPAS.Infrastructure.Repositories
{
    public class ResearchAreaRepository : IResearchAreaRepository
    {
        private readonly ApplicationDbContext _context;

        public ResearchAreaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResearchArea>> GetAllAsync()
        {
            return await _context.ResearchAreas.ToListAsync();
        }

        public async Task<ResearchArea?> GetByIdAsync(int id)
        {
            return await _context.ResearchAreas.FindAsync(id);
        }

        public async Task AddAsync(ResearchArea researchArea)
        {
            await _context.ResearchAreas.AddAsync(researchArea);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ResearchArea researchArea)
        {
            _context.ResearchAreas.Update(researchArea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var researchArea = await _context.ResearchAreas.FindAsync(id);
            if (researchArea != null)
            {
                _context.ResearchAreas.Remove(researchArea);
                await _context.SaveChangesAsync();
            }
        }
    }
}
