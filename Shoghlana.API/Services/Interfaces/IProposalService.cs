using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.API.Services.Implementations;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Interfaces;

public interface IProposalService : IGenericService<Proposal>
{
    public Task<ActionResult<GeneralResponse>> GetAllAsync();

    public ActionResult<GeneralResponse> GetById(int id);

    public Task<ActionResult<GeneralResponse>> GetByJobIdAsync(int id);

    public Task<ActionResult<GeneralResponse>> GetByFreelancerIdAsync(int id);

    public Task<ActionResult<GeneralResponse>> AddAsync([FromForm] AddProposalDTO addProposalDTO);

    // TODO : Try To use Async in Find to reduce waiting time
    public Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] AddProposalDTO addProposalDTO);

    public ActionResult<GeneralResponse> Delete(int id);

    public ActionResult<GeneralResponse> AcceptProposal(int proposalId);

    ActionResult<GeneralResponse> RejectProposal(int proposalId);

}
