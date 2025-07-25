using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Application.DTOs;
using Shoghlana.Domain.Entities;

namespace Shoghlana.API.Services.Interfaces;

public interface IProjectService : IGenericService<Project>
{
    ActionResult<GeneralResponse> GetAll();

    ActionResult<GeneralResponse> GetById(int id);

    ActionResult<GeneralResponse> GetByFreelancerId(int id);

    Task<ActionResult<GeneralResponse>> AddAsync([FromForm] AddProjectDTO projectDTO);

    Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] AddProjectDTO updatedProjectDTO);

    ActionResult<GeneralResponse> Delete(int id);

}
