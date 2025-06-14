using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;
public class PaginatedJobsRequestBodyDTO
{
    public int[]? CategoriesIDs { get; set; } = null;

    public string[]? Includes { get; set; } = ["Proposals"];

}
