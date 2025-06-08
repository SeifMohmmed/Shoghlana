using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class FreelancerService : GenericService<Freelancer> , IFreelancerService
{
    public FreelancerService(IUnitOfWork unitOfWork, IGenericRepository<Freelancer> repository)
        : base(unitOfWork, repository)
    {

    }
}
