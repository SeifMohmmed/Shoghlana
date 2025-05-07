using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddFreelancerWithProposalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerSkill_Skills_skillsId",
                table: "FreelancerSkill");

            migrationBuilder.DropTable(
                name: "ProjectSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreelancerSkill",
                table: "FreelancerSkill");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerSkill_skillsId",
                table: "FreelancerSkill");

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Proposals");

            migrationBuilder.RenameColumn(
                name: "skillsId",
                table: "FreelancerSkill",
                newName: "SkillsId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Proposals",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Proposals",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreelancerSkill",
                table: "FreelancerSkill",
                columns: new[] { "SkillsId", "freelancersId" });

            migrationBuilder.CreateTable(
                name: "projectSkills",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectSkills", x => new { x.ProjectId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_projectSkills_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_projectSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, null, "C#" },
                    { 2, null, "LINQ" },
                    { 3, null, "EF" },
                    { 4, null, "OOP" },
                    { 5, null, "Agile" },
                    { 6, null, "Blazor" }
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_VALUE_RANGE",
                table: "Rates",
                sql: "[Value] BETWEEN 1 AND 5");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerSkill_freelancersId",
                table: "FreelancerSkill",
                column: "freelancersId");

            migrationBuilder.CreateIndex(
                name: "IX_projectSkills_SkillId",
                table: "projectSkills",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerSkill_Skills_SkillsId",
                table: "FreelancerSkill",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerSkill_Skills_SkillsId",
                table: "FreelancerSkill");

            migrationBuilder.DropTable(
                name: "projectSkills");

            migrationBuilder.DropCheckConstraint(
                name: "CK_VALUE_RANGE",
                table: "Rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreelancerSkill",
                table: "FreelancerSkill");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerSkill_freelancersId",
                table: "FreelancerSkill");

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.RenameColumn(
                name: "SkillsId",
                table: "FreelancerSkill",
                newName: "skillsId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Proposals",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Proposals",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreelancerSkill",
                table: "FreelancerSkill",
                columns: new[] { "freelancersId", "skillsId" });

            migrationBuilder.CreateTable(
                name: "ProjectSkill",
                columns: table => new
                {
                    projectsId = table.Column<int>(type: "int", nullable: false),
                    skillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkill", x => new { x.projectsId, x.skillsId });
                    table.ForeignKey(
                        name: "FK_ProjectSkill_Projects_projectsId",
                        column: x => x.projectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkill_Skills_skillsId",
                        column: x => x.skillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "ClientId", "Description", "ExperienceLevel", "FreelancerId", "MaxBudget", "MinBudget", "PostTime", "Title" },
                values: new object[,]
                {
                    { 1, null, 1, "Develop software applications", 1, null, 2000m, 1000m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Software Developer" },
                    { 2, null, 1, "Develop software applications", 1, null, 2000m, 1000m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BackEnd Developer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerSkill_skillsId",
                table: "FreelancerSkill",
                column: "skillsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkill_skillsId",
                table: "ProjectSkill",
                column: "skillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerSkill_Skills_skillsId",
                table: "FreelancerSkill",
                column: "skillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
