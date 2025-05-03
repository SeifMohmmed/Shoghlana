using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Repositories;
public class JobSkillsRepository : Repository<JobSkills>, IJobSkillsRepository
{
    public JobSkillsRepository(ApplicationDbContext context) : base(context)
    { }
}
