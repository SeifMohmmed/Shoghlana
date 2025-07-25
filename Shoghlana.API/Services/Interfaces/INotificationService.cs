using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Domain.Entities;

namespace Shoghlana.API.Services.Interfaces;

public interface INotificationService : IGenericService<Notification>
{
    ActionResult<GeneralResponse> GetByFreelancerId(int FreelancerId);

    ActionResult<GeneralResponse> GetByClientId(int ClientId);

}
