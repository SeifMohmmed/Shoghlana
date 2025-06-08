using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class JobService : GenericService<Job>, IJobService
{
    public JobService(IUnitOfWork unitOfWork, IGenericRepository<Job> repository)
        : base(unitOfWork, repository)
    {

    }
}
