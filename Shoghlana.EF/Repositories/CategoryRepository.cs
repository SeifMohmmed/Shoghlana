using Microsoft.EntityFrameworkCore;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Repositories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Category? GetCategoryWithJobs(int id)
    {
        return _context.Category
            .Include(c => c.Jobs)
            .FirstOrDefault(x=>x.Id==id);
    }
}
