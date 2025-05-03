using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Repositories;
public class JobRepository : Repository<Job>, IJobRepository
{
    public JobRepository(ApplicationDbContext context) : base(context)
    { }
}
