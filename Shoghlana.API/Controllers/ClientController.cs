using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.DTOs;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var clients = _unitOfWork.client.GetAll().ToList();
        if (clients != null)
        {
            var clientsDTO = new List<GetClientDTO>();

            foreach (var client in clients)
            {
                var clientDTO = new GetClientDTO();

                clientDTO.Name = client.Name;
                clientDTO.Phone = client.Phone;
                clientDTO.Description = client.Description;
                clientDTO.Image = client.Image;
                clientDTO.Country = client.Country;

                clientsDTO.Add(clientDTO);
            }
            return Ok(new GeneralResponse()
            {
                IsSuccess = true,
                Data = clientsDTO
            });
        }
        else
        {
            return BadRequest(new GeneralResponse()
            {
                IsSuccess = false,
                Message = "There is no Clients"
            });
        }
    }
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var client = _unitOfWork.client.GetById(id);

        if (client != null)
        {
            var clientDTO = new GetClientDTO();

            clientDTO.Name = client.Name;
            clientDTO.Phone = client.Phone;
            clientDTO.Description = client.Description;
            clientDTO.Image = client.Image;
            clientDTO.Country = client.Country;

            return Ok(new GeneralResponse()
            {
                IsSuccess = true,
                Data = clientDTO
            });
        }
        else
        {
            return NotFound(new GeneralResponse()
            {
                IsSuccess = false,
                Message = $"There is no Client with This ID !"

            });
        }
    }
    [HttpGet("jobs/{id:int}")]
    public IActionResult GetJobByClientId(int id)
    {
        var client = _unitOfWork.client.GetClientWithJobs(id);
        if (client != null)
        {
            var clientWithJobsDTO = new ClientWithJobsDTO();

            clientWithJobsDTO.Name = client.Name;
            clientWithJobsDTO.Image = client.Image;
            clientWithJobsDTO.Jobs = client.Jobs.Select(job => new JobDTO
            {
                Title = job.Title,
                Description = job.Description,
                MaxBudget = job.MaxBudget,
                MinBudget = job.MinBudget,
                PostTime = job.PostTime,
            }).ToList();

            return Ok(new GeneralResponse()
            {
                IsSuccess = true,
                Data = clientWithJobsDTO
            });
        }
        else
        {
            return NotFound(new GeneralResponse()
            {
                IsSuccess = false,
                Message = "Client Not Found"
            });
        }
    }
    [HttpPost]
    public IActionResult AddClient([FromForm] ClientDTO clientDTO)
    {
        using var dataStream = new MemoryStream();

        clientDTO.Image?.CopyTo(dataStream);

        if (ModelState.IsValid)
        {
            var client = new Client();

            client.Name = clientDTO.Name;
            client.Phone = clientDTO.Phone;
            client.Country = clientDTO.Country;
            client.Description = clientDTO.Description;
            client.Image = dataStream.ToArray();

            _unitOfWork.client.Add(client);
            _unitOfWork.Save();

            return Ok(new GeneralResponse()
            {
                IsSuccess = true,
                Message = "Client Added Successfully !",
                Data = clientDTO
            });
        }
        else
        {
            return BadRequest(new GeneralResponse()
            {
                IsSuccess = false,
                Message = "Invalid Client Data"
            });
        }
    }
    [HttpPut("{id:int}")]
    public IActionResult UpdateClient(int id, [FromForm] ClientDTO clientDTO)
    {
        using var dataStream = new MemoryStream();

        clientDTO.Image?.CopyTo(dataStream);

        var client = _unitOfWork.client.GetById(id);
        if (client == null)
        {
            return BadRequest(new GeneralResponse()
            {
                IsSuccess = false,
                Message = $"There is no Client with This ID !"
            });
        }
        client.Name = clientDTO.Name;
        client.Phone = clientDTO.Phone;
        client.Country = clientDTO.Country;
        client.Description = clientDTO.Description;

        client.Image = dataStream.ToArray();
        _unitOfWork.client.Update(client);
        _unitOfWork.Save();

        return Ok(new GeneralResponse()
        {
            IsSuccess = true,
            Message = "Client updated",
            Data = clientDTO
        });
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteClient(int id)
    {
        var client = _unitOfWork.client.GetById(id);

        if (client == null)
        {
            return BadRequest(new GeneralResponse
            {
                IsSuccess = false,
                Message = $"There is no Client with This ID !"
            });
        }

        _unitOfWork.client.Delete(client);
        _unitOfWork.Save();

        return Ok(new GeneralResponse
        {
            IsSuccess = true,
            Message = "Client Deleted Successfully !"
        });

    }
}
