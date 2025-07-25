using Shoghlana.API.Response;
using Shoghlana.Domain.Entities;

namespace Shoghlana.API.Services.Interfaces;

public interface ISkillService : IGenericService<Skill>
{
    Task<GeneralResponse> GetAllAsync();

    Task<GeneralResponse> GetByIdAsync(int id);

}
