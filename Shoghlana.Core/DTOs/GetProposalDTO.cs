using Shoghlana.Core.Enums;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;
public class GetProposalDTO
{
    //  [Key]
    public int Id { get; set; }

    public DateTime ApprovedTime { get; set; } // known when the client approves

    public DateTime Deadline { get; set; } // calulated after approve

    public string? Description { get; set; }

    public decimal Price { get; set; }

    // public ProposalStatus Status { get; set; } //  not in the get DTO but assigned from backend when adding a proposal

    public List<string>? ReposLinks { get; set; }

    //---------------------------------
    //public List<ProposalImages>? Image { get; set; }

    public List<GetPropsalImageDTO>? Images { get; set; }

    public int FreelancerId { get; set; }

    // public Freelancer Freelancer { get; set; }

    public int? JobId { get; set; }

    // public Job Job { get; set; }
}
