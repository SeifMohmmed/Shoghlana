using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.Core.Interfaces;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RateController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public RateController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}
