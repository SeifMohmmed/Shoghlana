using Shoghlana.Domain.Enums;

namespace Shoghlana.Application.DTOs;
public class GetProposalDTO
{
    public int Id { get; set; }

    public DateTime ApprovedTime { get; set; }

    public DateTime Deadline { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public List<string>? ReposLinks { get; set; }

    //---------------------------------

    public List<GetPropsalImageDTO>? Images { get; set; }

    public int FreelancerId { get; set; }

    public string? FreelancerName { get; set; }

    public string? FreelancerTitle { get; set; }

    // public Freelancer Freelancer { get; set; }

    public int? JobId { get; set; }

    // public Job Job { get; set; }

    public ProposalStatus Status { get; set; }

    public string? JobTitle { get; set; }

    public string? ClientName { get; set; }


}
