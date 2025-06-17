using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Interfaces;
using Shoghlana.Core.DTOs;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RateController : ControllerBase
{
    private readonly IRateService _rateService;

    public RateController(IRateService rateService)
    {
        _rateService = rateService;
    }


    [HttpGet]
    public ActionResult<GeneralResponse> GetAll()
    {
        return _rateService.GetAll();
    }


    [HttpGet("id")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        return _rateService.GetById(id);
    }


    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> CreateRate(RateDTO rateDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Invalid rate data"
            };
        }

        return await _rateService.CreateRateAsync(rateDTO);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<GeneralResponse>> UpdateRate(int id, RateDTO rateDTO)
    {

        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Message = "Invalid rate data"
            };
        }

        return await _rateService.UpdateRateAsync(id, rateDTO);
    }

    [HttpDelete("id")]
    public ActionResult<GeneralResponse> DeleteRate(int id)
    {
        return _rateService.DeleteRate(id);
    }
}
