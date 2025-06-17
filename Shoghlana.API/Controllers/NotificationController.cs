using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Implementations;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.Interfaces;
using Shoghlana.EF.Hubs;

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
