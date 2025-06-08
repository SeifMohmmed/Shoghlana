//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Shoghlana.Core.Interfaces;
//using Shoghlana.EF.Hubs;

//namespace Shoghlana.API.Controllers;
//[Route("api/[controller]")]
//[ApiController]
//public class ClientNotificationController : ControllerBase
//{
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly IMapper _mapper;
//    private readonly IHubContext<NotificationHub> _hubContext;

//    public ClientNotificationController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationHub> hubContext)
//    {
//        _unitOfWork = unitOfWork;
//        _mapper = mapper;
//        _hubContext = hubContext;
//    }
//}
