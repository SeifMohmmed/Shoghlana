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
public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(s => s.Title).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Description).IsRequired(false).HasMaxLength(100);

        builder.HasData(
            new List<Skill>()
            {
        new Skill { Id = 1, Title = "Graphic Design" },
        new Skill { Id = 2, Title = "Industrial Drawing" },
        new Skill { Id = 3, Title = "Web Design" },
        new Skill { Id = 4, Title = "Brand Identity Design" },
        new Skill { Id = 5, Title = "Product Design" },
        new Skill { Id = 6, Title = "Logo Design" },
        new Skill { Id = 7, Title = "Mobile App Development" },
        new Skill { Id = 8, Title = "Web Development" },
        new Skill { Id = 9, Title = "Game Development" },
        new Skill { Id = 10, Title = "Computer Programming" },
        new Skill { Id = 11, Title = "Content Writing" },
        new Skill { Id = 12, Title = "Article Writing" },
        new Skill { Id = 13, Title = "Translation" },
        new Skill { Id = 14, Title = "Proofreading" },
        new Skill { Id = 15, Title = "Technical Writing" },
        new Skill { Id = 16, Title = "Digital Marketing" },
        new Skill { Id = 17, Title = "Search Engine Optimization (SEO)" },
        new Skill { Id = 18, Title = "Social Media Advertising" },
        new Skill { Id = 19, Title = "Email Marketing" },
        new Skill { Id = 20, Title = "Content Marketing" },
        new Skill { Id = 21, Title = "Technical Support" },
        new Skill { Id = 22, Title = "Network Administration" },
        new Skill { Id = 23, Title = "System Maintenance" },
        new Skill { Id = 24, Title = "Desktop Support" },
        new Skill { Id = 25, Title = "Cloud Computing Services" },
        new Skill { Id = 26, Title = "Educational Software Development" },
        new Skill { Id = 27, Title = "Curriculum Design" },
        new Skill { Id = 28, Title = "E-Learning Development" },
        new Skill { Id = 29, Title = "Lesson Design" },
        new Skill { Id = 30, Title = "Online Teaching" },
        new Skill { Id = 31, Title = "Advertisement Design" },
        new Skill { Id = 32, Title = "User Interface (UI) Design" },
        new Skill { Id = 33, Title = "User Experience (UX)" },
        new Skill { Id = 34, Title = "3D Modeling" },
        new Skill { Id = 35, Title = "Character Design" },
        new Skill { Id = 36, Title = "App Development with React.js" },
        new Skill { Id = 37, Title = "App Development with Node.js" },
        new Skill { Id = 38, Title = "App Development with Ruby on Rails" },
        new Skill { Id = 39, Title = "App Development with SQL" },
        new Skill { Id = 40, Title = "App Development with Django" },
        new Skill { Id = 41, Title = "Legal Article Writing" },
        new Skill { Id = 42, Title = "Creative Writing" },
        new Skill { Id = 43, Title = "Legal Verification" },
        new Skill { Id = 44, Title = "Localization" },
        new Skill { Id = 45, Title = "Market Analysis" },
        new Skill { Id = 46, Title = "Statistical Analysis" },
        new Skill { Id = 47, Title = "Performance Marketing" },
        new Skill { Id = 48, Title = "Affiliate Marketing" },
        new Skill { Id = 49, Title = "Online Marketing" },
        new Skill { Id = 50, Title = "Ad Campaign Management" }
            }
        );

    }
}
