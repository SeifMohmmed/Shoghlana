using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.AspNetCore.SignalR.Hubs;
using Shoghlana.Domain.Repositories;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly INotificationService _notificationService;

    public NotificationController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationHub> hubContext,
           INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hubContext = hubContext;
        _notificationService = notificationService;
    }

    [HttpGet("FreelancerId/{id:int}")]
    public ActionResult<GeneralResponse> GetByFreelancerId(int id)
    {
        return _notificationService.GetByFreelancerId(id);
    }

    [HttpGet("ClientId/{id:int}")]
    public ActionResult<GeneralResponse> GetByClientId(int id)
    {
        return _notificationService.GetByClientId(id);
    }

}
