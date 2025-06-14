using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.DTOs;
public class CategoryDTO
{
    public int Id { get; set; }

    public string Title { get; set; }

    public List<AddJobDTO>? Jobs { get; set; }
}
