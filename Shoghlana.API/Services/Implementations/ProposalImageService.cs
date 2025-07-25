using Shoghlana.API.Services.Interfaces;
using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;

namespace Shoghlana.API.Services.Implementations;

public class ProposalImageService : GenericService<ProposalImages>, IProposalImageService
{
    public ProposalImageService(IUnitOfWork unitOfWork, IGenericRepository<ProposalImages> genericRepository)
            : base(unitOfWork, genericRepository)
    {

    }

}
