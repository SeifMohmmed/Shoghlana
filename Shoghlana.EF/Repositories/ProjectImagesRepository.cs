using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
public class ProjectImagesRepository : GenericRepository<ProjectImages>, IProjectImagesRepository
{
    public ProjectImagesRepository(ApplicationDbContext context) : base(context)
    { }
}
