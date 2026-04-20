using System.Collections.Generic;
using System.Threading.Tasks;
using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Core.Interfaces
{
    public interface IResearchAreaRepository
    {
        Task<IEnumerable<ResearchArea>> GetAllAsync();
        Task<ResearchArea?> GetByIdAsync(int id);
        Task AddAsync(ResearchArea researchArea);
        Task UpdateAsync(ResearchArea researchArea);
        Task DeleteAsync(int id);
    }
}
