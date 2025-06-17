using Microsoft.EntityFrameworkCore;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Repositories;
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
            .FirstOrDefault(x=>x.Id==id);
    }
}
