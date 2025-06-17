using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
public class SkillRepository : GenericRepository<Skill>, ISkillRepository
{
    public SkillRepository(ApplicationDbContext context) : base(context)
    { }
}
