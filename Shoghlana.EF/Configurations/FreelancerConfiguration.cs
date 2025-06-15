using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoghlana.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoghlana.EF.Configurations;
internal class FreelancerConfiguration : IEntityTypeConfiguration<Freelancer>
{
    public void Configure(EntityTypeBuilder<Freelancer> builder)
    {
        builder.Property(f => f.Name).IsRequired().HasMaxLength(50);

        builder.Property(f => f.Title).IsRequired(false).HasMaxLength(50);

        builder.Property(f => f.Address).IsRequired(false).HasMaxLength(50);

        builder.Property(f => f.Overview).IsRequired(false).HasMaxLength(500);


        builder.HasData(
        new Freelancer
        {
            Id = 1,
            Name = "Mohamed Ahmed",
            Title = "Specialized Application Developer",
            Address = "Cairo, Egypt",
            Overview = "A professional developer with experience in web and mobile application development."
        },
        new Freelancer
        {
            Id = 2,
            Name = "Fatima Ali",
            Title = "Professional Graphic Designer",
            Address = "Riyadh, Saudi Arabia",
            Overview = "A highly experienced designer in logo and poster design."
        },
        new Freelancer
        {
            Id = 3,
            Name = "Ahmed Khaled",
            Title = "AI Specialist Programmer",
            Address = "Cairo, Egypt",
            Overview = "A programmer with experience in developing advanced applications using AI technologies."
        },
        new Freelancer
        {
            Id = 4,
            Name = "Sarah Hussein",
            Title = "Creative Abstract Designer",
            Address = "Dubai, United Arab Emirates",
            Overview = "A graphic designer with experience in abstract design and creative arts."
        },
        new Freelancer
        {
            Id = 5,
            Name = "Abdulrahman Mahmoud",
            Title = "Advanced Web Developer",
            Address = "Alexandria, Egypt",
            Overview = "A professional developer with experience in building and developing large and complex websites."
        },
        new Freelancer
        {
            Id = 6,
            Name = "Rima Abdullah",
            Title = "Professional Graphic Designer",
            Address = "Jeddah, Saudi Arabia",
            Overview = "A graphic designer with extensive experience in logo and brand identity design."
        },
        new Freelancer
        {
            Id = 7,
            Name = "Mahmoud Ali",
            Title = "Mobile App Developer",
            Address = "Cairo, Egypt",
            Overview = "A specialized developer with experience in mobile application development using the latest technologies."
        },
        new Freelancer
        {
            Id = 8,
            Name = "Noor Abdullah",
            Title = "Professional App Developer",
            Address = "Riyadh, Saudi Arabia",
            Overview = "A developer with experience in advanced web and mobile application development."
        },
        new Freelancer
        {
            Id = 9,
            Name = "Layla Mohammed",
            Title = "Creative Graphic Designer and Artist",
            Address = "Alexandria, Egypt",
            Overview = "A graphic designer and artist with experience in illustration and fine arts design."
        },
        new Freelancer
        {
            Id = 10,
            Name = "Ali Al-Husseini",
            Title = "Electronic Application Developer",
            Address = "Manama, Bahrain",
            Overview = "A developer with experience in web and mobile application development using multiple languages."
        }
        );

    }
}
