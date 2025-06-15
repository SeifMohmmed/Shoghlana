using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class AlterFreelancerTitleColumnAndSeedDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Freelancers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77b5a044-5c9e-495e-8442-76a816c17a66", "89dfb7b5-d529-40d9-92f6-808adb869512", "Admin", "ADMIN" },
                    { "82841029-737c-4234-9b74-64e448755ee4", "4535993f-2b05-49a0-a746-f2b830671da5", "Client", "CLIENT" },
                    { "8f072cb2-2b03-4fb0-b15a-e2168fdb5f48", "811a2238-0931-40ba-aedf-129fb1c8a22d", "Freelancer", "FREELANCER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77b5a044-5c9e-495e-8442-76a816c17a66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82841029-737c-4234-9b74-64e448755ee4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f072cb2-2b03-4fb0-b15a-e2168fdb5f48");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Freelancers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
