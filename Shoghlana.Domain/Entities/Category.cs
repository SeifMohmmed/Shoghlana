namespace Shoghlana.Domain.Entities;
public class Category
{
    //  [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; } = string.Empty;

    public List<Job>? Jobs { get; set; }
}
