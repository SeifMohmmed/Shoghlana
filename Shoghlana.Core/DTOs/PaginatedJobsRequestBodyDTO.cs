namespace Shoghlana.Core.DTOs;
public class PaginatedJobsRequestBodyDTO
{
    public int[]? CategoriesIDs { get; set; } = null;

    public string[]? Includes { get; set; } = null;

}
