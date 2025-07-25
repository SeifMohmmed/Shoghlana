using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Application.DTOs;
using Shoghlana.Domain.Entities;

namespace Shoghlana.API.Services.Interfaces;

public interface IFreelancerService : IGenericService<Freelancer>
{
    public ActionResult<GeneralResponse> GetAll();

    public ActionResult<GeneralResponse> GetById(int id);

    public Task<ActionResult<GeneralResponse>> AddAsync(AddFreelancerDTO addFreelancerDTO);

    public Task<ActionResult<GeneralResponse>> UpdateAsync(int id, [FromForm] AddFreelancerDTO addFreelancerDTO);

    public ActionResult<GeneralResponse> Delete(int id);

    public ActionResult<GeneralResponse> GetNotificationByFreelancerId(int freelancerId);

}
