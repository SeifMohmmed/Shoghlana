using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
public class PropsalImageRepository : GenericRepository<ProposalImages>, IProposalImagesRepository
{
    public PropsalImageRepository(ApplicationDbContext context) : base(context)
    { }
}
