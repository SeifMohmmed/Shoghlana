using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;
using Shoghlana.Infrastructure.Repositories;

namespace Shoghlana.EF.Repositories;
public class RateRepository : GenericRepository<Rate>, IRateRepository
{
    public RateRepository(ApplicationDbContext context) : base(context)
    { }
}
