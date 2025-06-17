using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Implementations;

public class NotificationService : GenericService<Notification>, INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificationService(IUnitOfWork unitOfWork, IGenericRepository<Notification> repository, IMapper mapper) : base(unitOfWork, repository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public ActionResult<GeneralResponse> GetByClientId(int ClientId)
    {
        var Notifications = _unitOfWork.NotificationRepository
                            .FindAll(criteria: n => n.FreelancerId == ClientId)
                            .OrderByDescending(n => n.SentTime).ToList();

        if (Notifications.Any())
        {
            var NotificationsDTO = _mapper.Map<List<GetNotificationDTO>>(Notifications);

            foreach (var notification in NotificationsDTO)
            {
                notification.IsRead = true;
            }

            _unitOfWork.NotificationRepository.Save();

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = NotificationsDTO,
                Message = "Notifications for this client were retrieved successfully"
            };

        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Data = null,
            Message = "No found Notifications for this client"
        };
    }

    public ActionResult<GeneralResponse> GetByFreelancerId(int FreelancerId)
    {
        var Notifications = _unitOfWork.NotificationRepository
                            .FindAll(criteria: n => n.FreelancerId == FreelancerId)
                            .OrderByDescending(n => n.SentTime).ToList();

        if (Notifications.Any())
        {
            var NotificationsDTO = _mapper.Map<List<GetNotificationDTO>>(Notifications);

            foreach (var notification in NotificationsDTO)
            {
                notification.IsRead = true;
            }

            _unitOfWork.NotificationRepository.Save();

            return new GeneralResponse()
            {
                IsSuccess = true,
                Data = NotificationsDTO,
                Message = "Notifications for this freelancer were retrieved successfully"
            };

        }

        return new GeneralResponse()
        {
            IsSuccess = false,
            Data = null,
            Message = "No found Notifications for this freelancer"
        };

    }
}
