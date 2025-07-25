namespace Shoghlana.Domain.Entities;
public class Rate
{
    // [Key]
    public int Id { get; set; }

    // saeed : make feedback nullable
    public string? Feedback { get; set; }

    //   [Range(1 , 5)]
    public int? Value { get; set; }

    //--------------------------------------

    //  [ForeignKey("Job")]
    public int? JobId { get; set; }

    public Job Job { get; set; }
}
