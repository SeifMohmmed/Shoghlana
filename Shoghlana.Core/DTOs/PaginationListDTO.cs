using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;
public class PaginationListDTO<T>
{
    public IEnumerable<T> Items { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages { get; set; }

    public int CurrentPage { get; set; }

}
