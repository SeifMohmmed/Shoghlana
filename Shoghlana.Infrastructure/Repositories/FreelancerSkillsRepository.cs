using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class FreelancerSkillsRepository : GenericRepository<FreelancerSkills>, IFreelancerSkillsRepository
{
    public FreelancerSkillsRepository(ApplicationDbContext context) : base(context)
    { }
}
