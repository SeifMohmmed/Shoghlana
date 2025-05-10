using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class EditTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Freelancers_FreelancerId",
                table: "Proposals");

            migrationBuilder.AlterColumn<int>(
                name: "FreelancerId",
                table: "Proposals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Freelancers",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Rate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Rate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Rate",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Freelancers_FreelancerId",
                table: "Proposals",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Freelancers_FreelancerId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Freelancers");

            migrationBuilder.AlterColumn<int>(
                name: "FreelancerId",
                table: "Proposals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Freelancers_FreelancerId",
                table: "Proposals",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id");
        }
    }
}
