using System.ComponentModel.DataAnnotations;

namespace Shoghlana.Application.DTOs;
public class AddProposalDTO
{

    [Required(ErrorMessage = "Duration is Required!")]
    public double Duration { get; set; }

    [Required(ErrorMessage = "Description is Required!")]
    public string Description { get; set; }


    [Required(ErrorMessage = "Price is Required!")]
    public decimal Price { get; set; }

    public List<string>? ReposLinks { get; set; }

    //---------------------------------

    public List<AddPropsalImageDTO>? Images { get; set; }

    public int FreelancerId { get; set; }

    public int JobId { get; set; }

}
