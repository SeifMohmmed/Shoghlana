using Shoghlana.Domain.Entities;

namespace Shoghlana.Domain.Repositories;
public interface ICategoryRepository : IGenericRepository<Category>
{
    Category? GetCategoryWithJobs(int id);
}
