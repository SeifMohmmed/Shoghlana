using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43e0e661-bc87-41cb-8865-d2942590f0bd", "f37db9e5-310f-43c7-911a-13175601cfbf", "Admin", "ADMIN" },
                    { "d14bce5b-fb04-43e2-9f93-bdfbb430e78e", "5b5fb572-a893-4e1d-860b-165dc0e5d5c8", "Client", "CLIENT" },
                    { "d5ed44ff-f5b6-4b70-b837-3c9a6a49bc91", "175d71bb-bc5d-428f-ab98-9076b535c5f5", "Freelancer", "FREELANCER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43e0e661-bc87-41cb-8865-d2942590f0bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d14bce5b-fb04-43e2-9f93-bdfbb430e78e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5ed44ff-f5b6-4b70-b837-3c9a6a49bc91");
        }
    }
}
