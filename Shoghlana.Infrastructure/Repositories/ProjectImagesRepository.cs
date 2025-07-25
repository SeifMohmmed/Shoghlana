using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class ProjectImagesRepository : GenericRepository<ProjectImages>, IProjectImagesRepository
{
    public ProjectImagesRepository(ApplicationDbContext context) : base(context)
    { }
}
