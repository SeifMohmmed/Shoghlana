using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
public class RateRepository : GenericRepository<Rate>, IRateRepository
{
    public RateRepository(ApplicationDbContext context) : base(context)
    { }
}
