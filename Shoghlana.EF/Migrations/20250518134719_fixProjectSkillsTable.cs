using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class fixProjectSkillsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectSkills");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProjectSkills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectSkills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ProjectSkills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
