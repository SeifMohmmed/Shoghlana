using Shoghlana.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shoghlana.Application.DTOs;
public class ProposalDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Proposal Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Proposal Price is required")]
    public decimal Price { get; set; }

    public ProposalStatus Status { get; set; } = ProposalStatus.Waiting;

    //---------------------------------

    public int? FreelancerId { get; set; }

    public int? JobId { get; set; }

}
