namespace Shoghlana.API.DTOs;

public class JobDTO
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime PostTime { get; set; }

    public decimal MaxBudget { get; set; }

    public decimal MinBudget { get; set; }
}
