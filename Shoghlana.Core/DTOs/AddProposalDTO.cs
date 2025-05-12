using Shoghlana.Core.Enums;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;
public class AddProposalDTO
{
    //public int Id { get; set; }

    //public string? Title { get; set; }

    //public DateTime ApprovedTime { get; set; } // known when the client approves

    [Required(ErrorMessage ="Duration is Required!")]
    public double Duration { get; set; } // given from the freelancer

    [Required(ErrorMessage = "Description is Required!")]
    public string Description { get; set; }


    [Required(ErrorMessage = "Price is Required!")]
    public decimal Price { get; set; }

    //public ProposalStatus Status { get; set; } // not in the get DTO but assigned from backend when adding a proposal

    public List<string>? ReposLinks { get; set; }

    //---------------------------------
    //public List<ProposalImages>? Image { get; set; }

    public List<AddPropsalImageDTO>? Images { get; set; }

    public int FreelancerId { get; set; }

    //public Freelancer Freelancer { get; set; }

    public int JobId { get; set; }

    //public Job Job { get; set; }
}
