using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class PropsalImageRepository : GenericRepository<ProposalImages>, IProposalImagesRepository
{
    public PropsalImageRepository(ApplicationDbContext context) : base(context)
    { }
}
