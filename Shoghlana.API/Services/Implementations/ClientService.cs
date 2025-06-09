using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Hubs;

namespace Shoghlana.API.Services.Implementations;

public class ClientService : GenericService<Client>, IClientService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IMapper _mapper;
    private List<string> allowedExtensions = new List<string>() { ".jpg", ".png" };
    private long maxAllowedImageSize = 1_048_576;

    public ClientService(IUnitOfWork unitOfWork, IGenericRepository<Client> repository, IHubContext<NotificationHub> hubContext
        , IMapper mapper) : base(unitOfWork, repository)
    {
        _hubContext = hubContext;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var clients = _unitOfWork.clientRepository.FindAll();

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

                var notificationDto = new NotificationDTO
                {
                    Title = "New Client Registered",
                    Description = $"{client.Name} has registered.",
                    SentTime = DateTime.Now,
                    SenderName = client.Name,
                    SenderImage = client.Image
                };
                _hubContext.Clients.All.SendAsync("ReceiveNotification", notificationDto);

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
        //var client = _unitOfWork.clientRepository.GetById(id);

        Client? client = _unitOfWork.clientRepository.Find(c => c.Id == id, new string[] { "Jobs" });

        if (client != null)
        {
            var clientsDTO = new GetClientDTO();

            clientsDTO.Name = client.Name;
            clientsDTO.Phone = client.Phone;
            clientsDTO.Description = client.Description;
            clientsDTO.Image = client.Image;
            clientsDTO.Country = client.Country;
            clientsDTO.JobsCount = client.JobsCount;
            clientsDTO.CompletedJobsCount = client.CompletedJobsCount;
            clientsDTO.Id = client.Id;
            clientsDTO.RegisterationTime = client.RegisterationTime;

            if (client?.Jobs?.Count > 0)
            {
                foreach (Job job in client.Jobs)
                {
                    JobDTO jobDTO = new JobDTO();
                    jobDTO = _mapper.Map<Job, JobDTO>(job);
                    clientsDTO.Jobs.Add(jobDTO);
                }
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
                Status = 400,
                Message = $"There is no Client with This ID !"

            };
        }
    }


    [HttpGet("jobs/{id:int}")]
    public ActionResult<GeneralResponse> GetJobsByClientId(int id)
    {
        var client = _unitOfWork.clientRepository.Find(criteria: c => c.Id == id, includes: new string[] { "Jobs" });

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
    public async Task<ActionResult<GeneralResponse>> CreateClient([FromForm] ClientDTO clientDTO)
    {

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
                Message = "Only JPG and PNG image formats are allowed!"

            };
        }

        if (clientDTO.Image.Length > maxAllowedImageSize)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Image size exceeds the maximum allowed size (1 MB)!"
            };
        }

        using var dataStream = new MemoryStream();

        await clientDTO.Image.CopyToAsync(dataStream);

        var client = new Client()
        {

            Name = clientDTO.Name,
            Image = dataStream.ToArray(),
            Description = clientDTO.Description,
            Country = clientDTO.Country,
            Phone = clientDTO.Phone
        };

        _unitOfWork.clientRepository.Add(client);

        _unitOfWork.Save();

        // Send notificationAdd commentMore actions
        var notificationDto = new NotificationDTO
        {
            Title = "New Client Registered",
            Description = $"{client.Name} has registered.",
            SentTime = DateTime.Now,
            SenderName = client.Name,
            SenderImage = client.Image
        };

        await _hubContext.Clients.All.SendAsync("ReceiveNotification", notificationDto);

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

        if (clientDTO.Image.Length > maxAllowedImageSize)
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

        var existingClient = _unitOfWork.clientRepository.GetById(id);

        if (existingClient == null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Client Not Found !"
            };
        }

        existingClient.Name = clientDTO.Name;
        existingClient.Phone = clientDTO.Phone;
        existingClient.Country = clientDTO.Country;
        existingClient.Description = clientDTO.Description;
        existingClient.Image = dataStream.ToArray();

        _unitOfWork.clientRepository.Update(existingClient);
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
        var client = _unitOfWork.clientRepository.GetById(id);

        if (client == null)
        {
            return new GeneralResponse
            {
                IsSuccess = false,
                Status = 400,
                Message = "Client Not Found !"
            };
        }

        _unitOfWork.clientRepository.Delete(client);
        _unitOfWork.clientRepository.Save();

        return new GeneralResponse
        {
            IsSuccess = true,
            Status = 200,
            Message = "Client Deleted Successfully !"
        };
    }


}
