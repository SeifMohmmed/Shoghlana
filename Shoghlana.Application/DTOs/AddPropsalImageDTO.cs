using Microsoft.AspNetCore.Http;

namespace Shoghlana.Application.DTOs;
public class AddPropsalImageDTO
{
    public IFormFile? Image { get; set; }

    public int ProposalId { get; set; }
}
