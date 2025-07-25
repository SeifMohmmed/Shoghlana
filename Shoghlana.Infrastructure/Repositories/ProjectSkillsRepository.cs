using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class ProjectSkillsRepository : GenericRepository<ProjectSkills>, IProjectSkillsRepository
{
    public ProjectSkillsRepository(ApplicationDbContext context) : base(context)
    { }
}
