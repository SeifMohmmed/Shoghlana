﻿using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.Core.Interfaces;
public interface ICategoryRepository : IGenericRepository<Category>
{
    Category? GetCategoryWithJobs(int id);
}
