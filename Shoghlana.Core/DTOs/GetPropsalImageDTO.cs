using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;
public class GetPropsalImageDTO
{
    public int Id { get; set; }

    public byte[] Image { get; set; }

    //--------------------------------

    public int ProposalId { get; set; }
}
