using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Freelancer")]
public class SecuredController : ControllerBase
{
    [HttpGet]
    public IActionResult GetData()
    {
        return Ok("Hello from secured controller");
    }
}
