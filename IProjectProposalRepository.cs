using System.Collections.Generic;
using System.Threading.Tasks;
using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Core.Interfaces
{
    public interface IProjectProposalRepository
    {
        Task<IEnumerable<ProjectProposal>> GetAllAsync();
        Task<IEnumerable<ProjectProposal>> GetByStudentAsync(string studentId);
        Task<IEnumerable<ProjectProposal>> GetPendingByResearchAreasAsync(IEnumerable<int> researchAreaIds);
        Task<ProjectProposal?> GetByIdAsync(int id);
        Task AddAsync(ProjectProposal proposal);
        Task UpdateAsync(ProjectProposal proposal);
        Task DeleteAsync(int id);
    }
}
