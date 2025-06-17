using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
public class JobSkillsRepository : GenericRepository<JobSkills>, IJobSkillsRepository
{
    public JobSkillsRepository(ApplicationDbContext context) : base(context)
    { }
}
