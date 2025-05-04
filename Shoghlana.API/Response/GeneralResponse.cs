namespace Shoghlana.API.Response;

public class GeneralResponse 
{
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Changed to dynamic from generic to return different types in the same controller based on conditions.
    /// Use to send a failure message if failed, a DTO on success, or the object itself when added or edited.
    /// </summary>
    public dynamic? Data { get; set; }


    /// <summary>
    /// Use to add optional notes to the consumer for success, add, edit, or delete operations.  
    /// </summary>
    public string? Message { get; set; } = string.Empty;

    /// <summary>
    /// In case of failer the consumer can easily know the reason by checking on the common failer satus codes(404 , 500 ... etc) => and make handler for each one
    /// It's way better than checking the string message to know the failer reason 
    /// </summary>
    public int Status { get; set; }

    public string? Token { get; set; } = null;

    public DateTime? Expired { get; set; } = null;

}
