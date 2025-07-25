using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Application.DTOs;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }


    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        return _clientService.GetAll();
    }


    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        return _clientService.GetById(id);
    }


    [HttpGet("jobs/{id:int}")]
    public ActionResult<GeneralResponse> GetJobByClientId(int id)
    {
        return _clientService.GetJobsByClientId(id);
    }

    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> AddClient([FromForm] ClientDTO clientDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Data = ModelState,
                Message = "Invalid Model State !"
            };
        }

        return await _clientService.CreateClient(clientDTO);
    }


    [HttpPut]
    public async Task<ActionResult<GeneralResponse>> UpdateClient(ClientDTO clientDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Model State is Invalid !"
            };
        }

        return await _clientService.UpdateClient(clientDTO);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> DeleteClient(int id)
    {
        return _clientService.DeleteClient(id);
    }


    [HttpGet("Notification/{clientId:int}")]
    public ActionResult<GeneralResponse> GetNotificationByClientId(int clientId)
    {
        return
            _clientService.GetNotificationByClientId(clientId);
    }


}
