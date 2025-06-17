using Shoghlana.Core.Models;

namespace Shoghlana.Core.Interfaces;
public interface ICategoryRepository : IGenericRepository<Category>
{
    Category? GetCategoryWithJobs(int id);
}
