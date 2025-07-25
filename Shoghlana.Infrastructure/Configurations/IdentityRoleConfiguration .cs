using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shoghlana.Infrastructure.Configurations;
internal class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
                      new IdentityRole()
                      {
                          Id = "77b5a044-5c9e-495e-8442-76a816c17a66",
                          Name = "Admin",
                          NormalizedName = "Admin".ToUpper(),
                          ConcurrencyStamp = "89dfb7b5-d529-40d9-92f6-808adb869512"
                      },
                         new IdentityRole()
                         {
                             Id = "82841029-737c-4234-9b74-64e448755ee4",
                             Name = "Client",
                             NormalizedName = "Client".ToUpper(),
                             ConcurrencyStamp = "4535993f-2b05-49a0-a746-f2b830671da5"
                         },
                           new IdentityRole()
                           {
                               Id = "8f072cb2-2b03-4fb0-b15a-e2168fdb5f48",
                               Name = "Freelancer",
                               NormalizedName = "Freelancer".ToUpper(),
                               ConcurrencyStamp = "811a2238-0931-40ba-aedf-129fb1c8a22d"
                           }
                      );
    }
}
