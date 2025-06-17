using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<GeneralResponse> GetAllAsync()
    {
        return await _skillService.GetAllAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<GeneralResponse> GetByIdAsync(int id)
    {
        return await _skillService.GetByIdAsync(id);
    }
}
