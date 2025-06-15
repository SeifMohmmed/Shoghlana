using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        return _projectService.GetAll();
    }

    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        return _projectService.GetById(id);
    }

    [HttpGet("freelancer/{id:int}")]
    public ActionResult<GeneralResponse> GetFreelancerId(int id)
    {
        return _projectService.GetByFreelancerId(id);
    }

    [HttpPost("{id:int}")]
    public async Task<ActionResult<GeneralResponse>> AddProject([FromForm] AddProjectDTO projectDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse
            {
                Status = 400,
                IsSuccess = false,
                Message = "Model State is Invaild!"
            };
        }

        return await _projectService.AddAsync(projectDTO);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<GeneralResponse>> UpdateProject(int id, [FromForm] AddProjectDTO updatedProjectDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse
            {
                Status = 400,
                IsSuccess = false,
                Data = ModelState,
                Message = "Model State is Invaild!"
            };
        }

        return await _projectService.UpdateAsync(id, updatedProjectDTO);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> Delete(int id)
    {
        return _projectService.Delete(id);
    }
}
