using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoghlana.Core.Models;

namespace Shoghlana.EF.Configurations;
internal class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.RegisterationTime)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.Country)
            .HasMaxLength(100);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.HasMany(c => c.Notifications)
                  .WithOne(n => n.Client)
                  .HasForeignKey(n => n.ClientId);

        #region Seed Data
        builder.HasData(
           new Client
           {
               Id = 1,
               Name = "Abdulrahman Ahmed",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A programmer and application developer specialized in web development.",
               Country = "Saudi Arabia",
               Phone = "+966123456789"
           },
           new Client
           {
               Id = 2,
               Name = "Fatima Mohammed",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A professional graphic designer specializing in logo and poster design.",
               Country = "Egypt",
               Phone = "+201234567890"
           },
           new Client
           {
               Id = 3,
               Name = "Ali Alabdullah",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A professional digital marketer with experience in managing social media ad campaigns.",
               Country = "United Arab Emirates",
               Phone = "+971123456789"
           },
           new Client
           {
               Id = 4,
               Name = "Maryam Hassan",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A content writer specialized in creative writing and artistic articles.",
               Country = "Jordan",
               Phone = "+962123456789"
           },
           new Client
           {
               Id = 5,
               Name = "Yousef Khalid",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A photographer specializing in event and special occasion photography.",
               Country = "Iraq",
               Phone = "+964123456789"
           },
           new Client
           {
               Id = 6,
               Name = "Lama Abdullah",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A professional project manager in tech and software development.",
               Country = "Saudi Arabia",
               Phone = "+966123456789"
           },
           new Client
           {
               Id = 7,
               Name = "Omar Ahmed",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A creative content marketer working on promoting digital content for startups.",
               Country = "Egypt",
               Phone = "+201234567890"
           },
           new Client
           {
               Id = 8,
               Name = "Rana Mahmoud",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A professional app developer working in mobile app development.",
               Country = "Lebanon",
               Phone = "+961123456789"
           },
           new Client
           {
               Id = 9,
               Name = "Ahmed Ali",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A marketing manager specialized in digital marketing strategies.",
               Country = "Jordan",
               Phone = "+962123456789"
           },
           new Client
           {
               Id = 10,
               Name = "Huda Saleh",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "An expert in designing and managing websites for small and medium businesses.",
               Country = "Saudi Arabia",
               Phone = "+966123456789"
           },
           new Client
           {
               Id = 11,
               Name = "Salma Abdullah",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A professional financial accountant working in financial reporting.",
               Country = "United Arab Emirates",
               Phone = "+971123456789"
           },
           new Client
           {
               Id = 12,
               Name = "Mohammed Hassan",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "An architect specialized in residential building design.",
               Country = "Egypt",
               Phone = "+201234567890"
           },
           new Client
           {
               Id = 13,
               Name = "Zainab Abdullah",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A doctor specialized in pediatrics and mental health.",
               Country = "Iraq",
               Phone = "+964123456789"
           },
           new Client
           {
               Id = 14,
               Name = "Ahmed Hussein",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A creative graphic designer working in commercial ad design.",
               Country = "Lebanon",
               Phone = "+961123456789"
           },
           new Client
           {
               Id = 15,
               Name = "Fatima Ali",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A professional translator specializing in medical and scientific text translation.",
               Country = "Jordan",
               Phone = "+962123456789"
           },
           new Client
           {
               Id = 16,
               Name = "Abdullah Mahmoud",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A professional web developer in electronic application development.",
               Country = "Saudi Arabia",
               Phone = "+966123456789"
           },
           new Client
           {
               Id = 17,
               Name = "Reem Abdullah",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "An architectural engineer specialized in industrial facility design.",
               Country = "Egypt",
               Phone = "+201234567890"
           },
           new Client
           {
               Id = 18,
               Name = "Omar Hassan",
               RegisterationTime = new DateTime(2024, 12, 11),
               Description = "A financial accountant with extensive experience in financial accounting.",
               Country = "Lebanon",
               Phone = "+961123456789"
           }
       );
        #endregion

    }
}
