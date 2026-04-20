using System.Collections.Generic;
using System.Threading.Tasks;
using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Core.Interfaces
{
    public interface ISupervisorExpertiseRepository
    {
        Task<IEnumerable<SupervisorExpertise>> GetBySupervisorIdAsync(string supervisorId);
        Task AddAsync(SupervisorExpertise expertise);
        Task RemoveAsync(int expertiseId);
    }
}
