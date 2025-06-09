using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Interfaces;

public interface IProjectService : IGenericService<Project>
{
    ActionResult<GeneralResponse> GetAll();

    ActionResult<GeneralResponse> GetById(int id);

    Task<ActionResult<GeneralResponse>> AddAsync([FromForm] ProjectDTO projectDTO);

    Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] ProjectDTO updatedProjectDTO);

    ActionResult<GeneralResponse> Delete(int id);

}
