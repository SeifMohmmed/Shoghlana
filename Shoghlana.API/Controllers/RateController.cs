using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Shoghlana.API.Response;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.EF.Hubs;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RateController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHubContext<NotificationHub> _hubContext;

    public RateController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationHub> hubContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        var rates = _unitOfWork.rate.GetAll();

        if (rates != null)
        {
            var ratesDTO = new List<RateDTO>();

            foreach (var rate in rates)
            {
                var rateDTO = _mapper.Map<RateDTO>(rate);
                ratesDTO.Add(rateDTO);
            }

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Data = ratesDTO
            };
        }
        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 400,
            Message = "There is no Rates",
        };
    }

    [HttpGet("id")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        var rate = _unitOfWork.rate.GetById(id);

        if (rate != null)
        {
            var rateDTO = _mapper.Map<RateDTO>(rate);

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Data = rateDTO
            };
        }
        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 400,
            Message = "There is no Rates",
        };
    }

    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> CreateRate(RateDTO rateDTO)
    {
        if (ModelState.IsValid)
        {
            var existingJob = _unitOfWork.job.GetById(rateDTO.JobId);

            if (existingJob == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Status = 404,
                    Message = "Job Not Found",
                };
            }

            var rate = _mapper.Map<Rate>(rateDTO);
            _unitOfWork.rate.Add(rate);
            _unitOfWork.Save();

            if (existingJob.FreelancerId.HasValue)
            {
                var client = _unitOfWork.client.GetById(existingJob.ClientId.Value);

                if (client != null)
                {
                    var notificationDto = new NotificationDTO
                    {
                        Title = "New Rating Added for the Service",
                        SentTime = DateTime.Now,
                        Description = existingJob.Title,
                        SenderName = client.Name,
                        SenderImage = client.Image
                    };
                    await NotifyFreelancer(existingJob.FreelancerId.Value, notificationDto);
                }
            }
            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Data = rateDTO,
                Message = "Rate Created Successfully !"
            };
        }

        return BadRequest(new GeneralResponse()
        {
            IsSuccess = false,
            Status = 400,
            Message = "Invalid Rate Data"
        });
    }

    private async Task NotifyFreelancer(int freelancerId, NotificationDTO notificationDto)
    {
        string connectionId = NotificationHub.GetUserConnectionId(freelancerId.ToString());
        if (!string.IsNullOrEmpty(connectionId))
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", notificationDto);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GeneralResponse>> UpdateRate(int id, RateDTO rateDTO)
    {
        if (ModelState.IsValid)
        {
            var existingRate = _unitOfWork.rate.GetById(id);

            if (existingRate != null)
            {
                _mapper.Map(rateDTO, existingRate);
                _unitOfWork.rate.Update(existingRate);
                _unitOfWork.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Status = 200,
                    Data = rateDTO,
                    Message = "Rate Updated Successfully"
                };
            }

            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Rate not found"
            };
        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 400,
            Message = "Invalid rate data"
        };
    }

    [HttpDelete("id")]
    public ActionResult<GeneralResponse> DeleteRate(int id)
    {
        var existingRate = _unitOfWork.rate.GetById(id);

        if (existingRate != null)
        {
            _unitOfWork.rate.Delete(existingRate);
            _unitOfWork.Save();

            return new GeneralResponse()
            {
                IsSuccess = true,
                Status = 200,
                Message = "Rate Deleted Successfully !"
            };
        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Status = 400,
            Message = "Rate not found"
        };
    }
}
