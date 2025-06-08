using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Repositories;
public class FreelancerRepository : GenericRepository<Freelancer>, IFreelancerRepository
{
    public FreelancerRepository(ApplicationDbContext context) : base(context)
    { }
}
