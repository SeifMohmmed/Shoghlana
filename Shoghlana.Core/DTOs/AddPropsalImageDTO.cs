using Microsoft.AspNetCore.Http;

namespace Shoghlana.Core.DTOs;
public class AddPropsalImageDTO
{
    public IFormFile? Image { get; set; }

    public int ProposalId { get; set; }
}
