using Shoghlana.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.Models;
public class Proposal
{
    //  [Key]
    public int Id { get; set; }

    //public string Title { get; set; }

    public DateTime ApprovedTime { get; set; } // known when the client approves

    public double Deadline { get; set; } // given from the freelancer

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public ProposalStatus Status { get; set; } // not in the DTO

    public List<string>? ReposLinks { get; set; }

    //---------------------------------
    public List<ProposalImages>? Image { get; set; }

    public int? FreelancerId { get; set; }

    public Freelancer Freelancer { get; set; }

    public int? JobId { get; set; }

    public Job Job { get; set; }
}
