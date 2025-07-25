using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class SkillRepository : GenericRepository<Skill>, ISkillRepository
{
    public SkillRepository(ApplicationDbContext context) : base(context)
    { }
}
