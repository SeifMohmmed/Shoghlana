using Shoghlana.API.Response;
using Shoghlana.Core.Models;

namespace Shoghlana.API.Services.Interfaces;

public interface ISkillService : IGenericService<Skill>
{
    Task<GeneralResponse> GetAllAsync();

    Task<GeneralResponse> GetByIdAsync(int id);

}
