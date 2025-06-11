using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Configurations;
internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);


        builder.HasData(
            new Category
            {
                Id = 1,
                Title = "Design Services",
                Description = "Includes all services related to graphic design, industrial design, and web design."
            },
            new Category
            {
                Id = 2,
                Title = "Software Services",
                Description = "Includes development and programming of applications and software for various systems and devices."
            },
            new Category
            {
                Id = 3,
                Title = "Writing and Translation Services",
                Description = "Includes article writing, instant translation, and content creation for websites and blogs."
            },
            new Category
            {
                Id = 4,
                Title = "Digital Marketing Services",
                Description = "Includes managing digital marketing campaigns, social media advertising, and market analytics."
            },
            new Category
            {
                Id = 5,
                Title = "Technical Support Services",
                Description = "Includes user support, troubleshooting technical issues, and enhancing system and network performance."
            },
            new Category
            {
                Id = 6,
                Title = "Education and Training Services",
                Description = "Includes providing training courses, designing educational curricula, and developing learning resources."
            }
        );


    }
}
