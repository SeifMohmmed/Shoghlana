using Microsoft.AspNetCore.Mvc;
using Shoghlana.API.Response;
using Shoghlana.Core.DTOs;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Interfaces;

public interface IClientService : IGenericService<Client>
{
    public ActionResult<GeneralResponse> GetAll();

    public ActionResult<GeneralResponse> GetById(int id);

    public ActionResult<GeneralResponse> GetJobsByClientId(int id);

    public Task<ActionResult<GeneralResponse>> CreateClient([FromForm] ClientDTO clientDTO);

    public Task<ActionResult<GeneralResponse>> UpdateClient(int id, [FromForm] ClientDTO clientDTO);

    public ActionResult<GeneralResponse> DeleteClient(int id);

}
