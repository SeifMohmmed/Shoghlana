
using Shoghlana.API.Services.Implementations;
using Shoghlana.Core.Models;
using System.Linq.Expressions;
using Shoghlana.EF.Repositories;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Interfaces;

namespace Shoghlana.API.Services.Implementations;

public class ProposalService : GenericService<Proposal>, IProposalService
{
    public ProposalService(IUnitOfWork unitOfWork, IGenericRepository<Proposal> repository)
           : base(unitOfWork, repository)
        {
        }

    // Override generic methods if needed
    public override async Task<Proposal> GetByIdAsync(int id)
    {
        // Custom implementation for Proposal
        return await base.GetByIdAsync(id);
    }

    public async Task<Job> GetJobByIdAsync(int id)
    {
        return await _unitOfWork.jobRepository.GetByIdAsync(id);
    }

    public async Task<Freelancer> GetFreelancerByIdAsync(int id)
    {
        return await _unitOfWork.freelancerRepository.GetByIdAsync(id);
    }

    // Add any entity-specific methods here if needed
}
