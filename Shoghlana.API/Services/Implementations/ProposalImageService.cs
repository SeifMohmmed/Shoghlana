using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class ProposalImageService : GenericService<ProposalImages> , IProposalImageService
{
    public ProposalImageService(IUnitOfWork unitOfWork, IGenericRepository<ProposalImages> genericRepository)
            : base(unitOfWork, genericRepository)
        {

        }

    //**************************************



}
