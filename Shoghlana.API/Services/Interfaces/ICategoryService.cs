using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Interfaces;

public interface ICategoryService : IGenericService<Category>
{
    public ActionResult<GeneralResponse> GetAll();

    public ActionResult<GeneralResponse> GetById(int id);

    public ActionResult<GeneralResponse> CreateCategory(GetTitleOfCategoryDTO getTitleofCategoryDTO);

    public ActionResult<GeneralResponse> UpdateCategory(int id, GetTitleOfCategoryDTO getTitleofCategoryDTO);

    public ActionResult<GeneralResponse> DeleteCategory(int id);

}
