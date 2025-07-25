using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class JobSkillsRepository : GenericRepository<JobSkills>, IJobSkillsRepository
{
    public JobSkillsRepository(ApplicationDbContext context) : base(context)
    { }
}
