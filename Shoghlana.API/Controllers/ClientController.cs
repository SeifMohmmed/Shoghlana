using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.DTOs;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Repositories;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private List<string> allowedExtensions = new List<string>() { ".jpg", ".png" };
    private long maxAllowedPersonalImageSize = 1_048_576;  // 1 MB = 1024 * 1024 bytes
    public ClientController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
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
            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Data = clientsDTO
            };
        }
        else
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Message = "There is no Clients !"
            };
        }
    }
    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
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

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Data = clientDTO
            };
        }
        else
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = $"There is no Client with This ID !"

            };
        }
    }
    [HttpGet("jobs/{id:int}")]
    public ActionResult<GeneralResponse> GetJobByClientId(int id)
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

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Data = clientWithJobsDTO
            };
        }
        else
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Client Not Found !"
            };
        }
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

        if (clientDTO.Image is null)
        {

            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Image is Required !"
            };
        }

        if (!allowedExtensions.Contains(Path.GetExtension(clientDTO.Image.FileName).ToLower()))
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The allowed Personal Image Extensions => {jpg , png}",
            };
        }

        if (clientDTO.Image.Length > maxAllowedPersonalImageSize)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The max Allowed Personal Image Size => 1 MB ",
            };
        }
        using var dataStream = new MemoryStream();

        await clientDTO.Image.CopyToAsync(dataStream);

        Client client = new Client();
        client.Name = clientDTO.Name;
        client.Image = dataStream.ToArray();
        client.Description = clientDTO.Description;
        client.Country = clientDTO.Country;
        client.Phone = clientDTO.Phone;
        _unitOfWork.client.Add(client);
        _unitOfWork.Save();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 201,
            Message = " Client Added Successfully !"
        };
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<GeneralResponse>> UpdateClient(int id, [FromForm] ClientDTO clientDTO)
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
        if (clientDTO.Image is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Image is Required !"
            };
        }

        if (!allowedExtensions.Contains(Path.GetExtension(clientDTO.Image.FileName).ToLower()))
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The allowed Personal Image Extensions => {jpg , png}",
            };
        }

        if (clientDTO.Image.Length > maxAllowedPersonalImageSize)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "The max Allowed Personal Image Size => 1 MB ",
            };
        }
        using var dataStream = new MemoryStream();

        await clientDTO.Image.CopyToAsync(dataStream);

        var client = _unitOfWork.client.GetById(id);
        if (client == null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Client Not Found !"
            };
        }
        client.Name = clientDTO.Name;
        client.Phone = clientDTO.Phone;
        client.Country = clientDTO.Country;
        client.Description = clientDTO.Description;

        client.Image = dataStream.ToArray();
        _unitOfWork.client.Update(client);
        _unitOfWork.Save();

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Message = "Client Updated Successfully !",
            Data = clientDTO
        };
    }
    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> DeleteClient(int id)
    {
        var client = _unitOfWork.client.GetById(id);

        if (client == null)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Status = 400,
                Message = "Client Not Found !"
            };
        }

        _unitOfWork.client.Delete(client);
        _unitOfWork.Save();

        return new GeneralResponse
        {
            IsSuccess = true,
            Status = 200,
            Message = "Client Deleted Successfully !"
        };

    }
}
