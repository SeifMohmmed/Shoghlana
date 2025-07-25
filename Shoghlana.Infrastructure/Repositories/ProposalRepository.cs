using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class ProposalRepository : GenericRepository<Proposal>, IProposalRepository
{
    public ProposalRepository(ApplicationDbContext context) : base(context)
    { }
}
