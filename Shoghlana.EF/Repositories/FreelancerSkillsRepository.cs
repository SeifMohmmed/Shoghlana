using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
public class FreelancerSkillsRepository : GenericRepository<FreelancerSkills> , IFreelancerSkillsRepository
{
    public FreelancerSkillsRepository(ApplicationDbContext context) : base(context)
    { }
}
