using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
public class ProjectSkillsRepository : GenericRepository<ProjectSkills>, IProjectSkillsRepository
{
    public ProjectSkillsRepository(ApplicationDbContext context) : base(context)
    { }
}
