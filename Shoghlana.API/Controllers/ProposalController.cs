using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoghlana.Core.DTOs;
using Shoghlana.API.Response;
using Shoghlana.Core.Interfaces;
using Shoghlana.Core.Models;
using Shoghlana.API.Services.Interfaces;
using System.Threading.Tasks;

namespace Shoghlana.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProposalController : ControllerBase
{
    private readonly IProposalService _proposalService;

    public ProposalController(IProposalService proposalService, IProposalImageService proposalImageService)
    {
        _proposalService = proposalService;
    }


    [HttpGet]
    public async Task<ActionResult<GeneralResponse>> GetAll()
    {
        return await _proposalService.GetAllAsync();
    }


    [HttpGet("{id:int}")]
    public ActionResult<GeneralResponse> GetById(int id)
    {
        return _proposalService.GetById(id);
    }


    [HttpGet("JobId/{id:int}")]
    public async Task<ActionResult<GeneralResponse>> GetByJobId(int id)
    {
        return await _proposalService.GetByJobIdAsync(id);
    }


    [HttpGet("freelancerId/{id:int}")]
    public async Task<ActionResult<GeneralResponse>> GetByFreelancerId(int id)
    {
        return await _proposalService.GetByFreelancerIdAsync(id);
    }


    [HttpPost]
    public async Task<ActionResult<GeneralResponse>> AddAsync([FromForm] AddProposalDTO addProposalDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Data = ModelState,
                Message = "Invalid Data!"
            };
        }

        return await _proposalService.AddAsync(addProposalDTO);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<GeneralResponse>> Update(int id, [FromForm] AddProposalDTO addProposalDTO)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponse()
            {
                IsSuccess = false,
                Status = 400,
                Data = ModelState,
                Message = "Invalid Data!"
            };
        }

        return await _proposalService.UpdateAsync(id, addProposalDTO);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<GeneralResponse> Delete(int id)
    {
        return _proposalService.Delete(id);
    }

    [HttpGet("Accept/{proposalId:int}")]
    public ActionResult<GeneralResponse> AcceptProposal(int proposalId)
    {
        return _proposalService.AcceptProposal(proposalId);
    }

}
