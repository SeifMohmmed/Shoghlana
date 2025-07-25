namespace Shoghlana.Application.DTOs;
public class CategoryDTO
{
    public int Id { get; set; }

    public string Title { get; set; }

    public List<AddJobDTO>? Jobs { get; set; }
}
