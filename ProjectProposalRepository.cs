using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Core.Interfaces;
using BlindMatchPAS.Infrastructure.Data;

namespace BlindMatchPAS.Infrastructure.Repositories
{
    public class ProjectProposalRepository : IProjectProposalRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectProposalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectProposal>> GetAllAsync()
        {
            return await _context.ProjectProposals
                .Include(p => p.ResearchArea)
                .Include(p => p.Student)
                .Include(p => p.MatchedSupervisor)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProjectProposal>> GetByStudentAsync(string studentId)
        {
            return await _context.ProjectProposals
                .Include(p => p.ResearchArea)
                .Include(p => p.MatchedSupervisor)
                .Where(p => p.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProjectProposal>> GetPendingByResearchAreasAsync(IEnumerable<int> researchAreaIds)
        {
            return await _context.ProjectProposals
                .Include(p => p.ResearchArea)
                .Where(p => p.Status == "Pending" && researchAreaIds.Contains(p.ResearchAreaId))
                .ToListAsync();
        }

        public async Task<ProjectProposal?> GetByIdAsync(int id)
        {
            return await _context.ProjectProposals
                .Include(p => p.ResearchArea)
                .Include(p => p.Student)
                .Include(p => p.MatchedSupervisor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(ProjectProposal proposal)
        {
            await _context.ProjectProposals.AddAsync(proposal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectProposal proposal)
        {
            _context.ProjectProposals.Update(proposal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var proposal = await _context.ProjectProposals.FindAsync(id);
            if (proposal != null)
            {
                _context.ProjectProposals.Remove(proposal);
                await _context.SaveChangesAsync();
            }
        }
    }
}
