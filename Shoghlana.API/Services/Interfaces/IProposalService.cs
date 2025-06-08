using Shoghlana.API.Services.Implementations;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Interfaces;

public interface IProposalService : IGenericService<Proposal> 
{
    public Task<Job> GetJobByIdAsync(int id);

    public Task<Freelancer> GetFreelancerByIdAsync(int id);
}
