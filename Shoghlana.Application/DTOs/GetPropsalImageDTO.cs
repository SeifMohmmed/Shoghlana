namespace Shoghlana.Application.DTOs;
public class GetPropsalImageDTO
{
    public int Id { get; set; }

    public byte[] Image { get; set; }

    //--------------------------------

    public int ProposalId { get; set; }
}
