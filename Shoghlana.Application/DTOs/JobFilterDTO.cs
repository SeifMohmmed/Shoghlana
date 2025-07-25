namespace Shoghlana.Application.DTOs;
public class JobFilterDTO
{

    public decimal MaxBudget { get; set; }

    public decimal MinBudget { get; set; }

    public int ClientId { get; set; }

    public int CategoryId { get; set; }

    public List<string>? SkillsTitles { get; set; }

    public int? FreelancerId { get; set; }

}
