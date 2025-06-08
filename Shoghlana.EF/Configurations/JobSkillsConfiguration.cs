﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Configurations;
internal class JobSkillsConfiguration : IEntityTypeConfiguration<JobSkills>
{
    public void Configure(EntityTypeBuilder<JobSkills> builder)
    {

        builder.HasKey(js => new { js.JobId, js.SkillId });

    }
}
