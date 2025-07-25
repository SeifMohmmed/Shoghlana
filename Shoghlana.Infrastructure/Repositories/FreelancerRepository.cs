using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class FreelancerRepository : GenericRepository<Freelancer>, IFreelancerRepository
{
    public FreelancerRepository(ApplicationDbContext context) : base(context)
    { }
}
