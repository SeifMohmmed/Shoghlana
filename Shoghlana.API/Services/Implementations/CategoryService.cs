using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class CategoryService : GenericService<Category>,ICategoryService
{
    public CategoryService(IUnitOfWork unitOfWork,IGenericRepository<Category> repository)
        :base(unitOfWork,repository)
    {
        
    }

}
