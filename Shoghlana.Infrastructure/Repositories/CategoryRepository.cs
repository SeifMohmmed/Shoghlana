using Microsoft.EntityFrameworkCore;
using Shoghlana.Domain.Entities;
using Shoghlana.Domain.Repositories;
using Shoghlana.Infrastructure.Persistence;

namespace Shoghlana.Infrastructure.Repositories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Category? GetCategoryWithJobs(int id)
    {
        return _context.Categories
            .Include(c => c.Jobs)
            .FirstOrDefault(x => x.Id == id);
    }
}
