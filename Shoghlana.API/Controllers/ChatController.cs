using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Services.Implementations;
using Shoghlana.Core.DTOs;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly ChatServices _chatServices;

    public ChatController(ChatServices chatServices)
    {
        _chatServices = chatServices;
    }

    [HttpPost("register-user")]
    public IActionResult RegisterUser([FromBody] ChatDTO model)
    {
        if(model is null || string.IsNullOrEmpty(model.Name))
        {
            return BadRequest("Invalid user data.");
        }

        if(_chatServices.AddUsersToList(model.Name))
        {
            return NoContent();
        }

        return BadRequest("This name is taken, please choose another name.");
    
    }


}
