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
    private List<string> allowedExtensions = new List<string>() { ".jpg", ".png", ".jpeg" };
    private long maxAllowedImageSize = 1_048_576;

    public ClientService(IUnitOfWork unitOfWork, IGenericRepository<Client> repository, IHubContext<NotificationHub> hubContext
        , IMapper mapper) : base(unitOfWork, repository)
    {
        _hubContext = hubContext;
        _mapper = mapper;
    }

    public ActionResult<GeneralResponse> GetAll()
    {
        var clients = _unitOfWork.clientRepository.FindAll();

        if (clients != null)
        {
            var clientsDTO = new List<GetClientDTO>();

            foreach (var client in clients)
            {
                var clientDTO = new GetClientDTO();

                clientDTO.Id = client.Id;
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
                _hubContext.Clients.All.SendAsync("ReceiveNotification", notificationDto);  // todo why send notifi.. for all clients , for this client instead ??? 

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
                    AddJobDTO jobDTO = new AddJobDTO();
                    jobDTO = _mapper.Map<Job, AddJobDTO>(job);
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

    public ActionResult<GeneralResponse> GetJobsByClientId(int id)
    {
        var client = _unitOfWork.clientRepository.Find(criteria: c => c.Id == id, includes: new string[] { "Jobs" });

        if (client != null)
        {
            var clientWithJobsDTO = new ClientWithJobsDTO();

            clientWithJobsDTO.Name = client.Name;
            clientWithJobsDTO.Image = client.Image;
            clientWithJobsDTO.Jobs = client.Jobs.Select(job => new AddJobDTO
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
                Message = "Only JPG , PNG and JPEG image formats are allowed!"

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

    public async Task<ActionResult<GeneralResponse>> UpdateClient(ClientDTO clientDTO)
    {
        var existingClient = _unitOfWork.clientRepository.GetById(clientDTO.Id);

        if (existingClient == null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Client Not Found"
            };
        }
        if (clientDTO.Image is not null)
        {
            if (!allowedExtensions.Contains(Path.GetExtension(clientDTO.Image.FileName).ToLower()))
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "Please use a (Jpg, Png, Jpeg) file",
                    Data = GetById(clientDTO.Id)
                };
            }

            if (clientDTO.Image.Length > maxAllowedImageSize)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 400,
                    Message = "The max Allowed Image Size => 1 MB ",
                };
            }

            using var dataStream = new MemoryStream();

            await clientDTO.Image.CopyToAsync(dataStream);

            existingClient.Image = dataStream.ToArray();

        }

        existingClient.Name = clientDTO.Name;
        existingClient.Description = clientDTO.Description;
        existingClient.Country = clientDTO.Country;
        // existingClient.Phone = clientDTO.Phone;

        _unitOfWork.clientRepository.Update(existingClient);

        _unitOfWork.Save();


        return GetById(clientDTO.Id);
    }


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

    public ActionResult<GeneralResponse> GetNotificationByClientId(int clientId)
    {
        var client = _unitOfWork.clientRepository.Find(c => c.Id == clientId, includes: ["Notifications"]);

        if (client is null)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = $"Invalid Client ID : {clientId}"
            };
        }

        if (client?.Notifications?.Count == 0)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = $"No Notifications found for this client: {clientId}"
            };
        }

        var clientNotificationDTOs = new List<GetNotificationDTO>();

        foreach (var notification in client.Notifications)
        {
            var clientNotificationDTO = _mapper.Map<Notification, GetNotificationDTO>(notification);

            clientNotificationDTOs.Add(clientNotificationDTO);
        }

        return new GeneralResponse()
        {
            IsSuccess = true,
            Status = 200,
            Message = $"Client with ID : {clientId} => All Notifications"
        };
    }
}
