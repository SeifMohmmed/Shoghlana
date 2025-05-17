using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobSkills_Jobs_JobId",
                table: "jobSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_jobSkills_Skills_SkillId",
                table: "jobSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_projectSkills_Projects_ProjectId",
                table: "projectSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_projectSkills_Skills_SkillId",
                table: "projectSkills");

            migrationBuilder.DropTable(
                name: "FreelancerSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projectSkills",
                table: "projectSkills");

            migrationBuilder.DropIndex(
                name: "IX_projectSkills_SkillId",
                table: "projectSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_jobSkills",
                table: "jobSkills");

            migrationBuilder.DropIndex(
                name: "IX_jobSkills_SkillId",
                table: "jobSkills");

            migrationBuilder.RenameTable(
                name: "projectSkills",
                newName: "ProjectSkills");

            migrationBuilder.RenameTable(
                name: "jobSkills",
                newName: "JobSkills");

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

            migrationBuilder.AlterColumn<byte[]>(
                name: "Poster",
                table: "Projects",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Freelancers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSkills",
                table: "ProjectSkills",
                columns: new[] { "SkillId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobSkills",
                table: "JobSkills",
                columns: new[] { "SkillId", "JobId" });

            migrationBuilder.CreateTable(
                name: "FreelancerSkills",
                columns: table => new
                {
                    FreelancerId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerSkills", x => new { x.SkillId, x.FreelancerId });
                    table.ForeignKey(
                        name: "FK_FreelancerSkills_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelancerSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkills_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId",
                table: "JobSkills",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerSkills_FreelancerId",
                table: "FreelancerSkills",
                column: "FreelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkills_Jobs_JobId",
                table: "JobSkills",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkills_Skills_SkillId",
                table: "JobSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkills_Projects_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkills_Skills_SkillId",
                table: "ProjectSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSkills_Jobs_JobId",
                table: "JobSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkills_Skills_SkillId",
                table: "JobSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkills_Projects_ProjectId",
                table: "ProjectSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkills_Skills_SkillId",
                table: "ProjectSkills");

            migrationBuilder.DropTable(
                name: "FreelancerSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSkills",
                table: "ProjectSkills");

            migrationBuilder.DropIndex(
                name: "IX_ProjectSkills_ProjectId",
                table: "ProjectSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobSkills",
                table: "JobSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobSkills_JobId",
                table: "JobSkills");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectSkills");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProjectSkills");

            migrationBuilder.RenameTable(
                name: "ProjectSkills",
                newName: "projectSkills");

            migrationBuilder.RenameTable(
                name: "JobSkills",
                newName: "jobSkills");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Poster",
                table: "Projects",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_projectSkills",
                table: "projectSkills",
                columns: new[] { "ProjectId", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_jobSkills",
                table: "jobSkills",
                columns: new[] { "JobId", "SkillId" });

            migrationBuilder.CreateTable(
                name: "FreelancerSkill",
                columns: table => new
                {
                    SkillsId = table.Column<int>(type: "int", nullable: false),
                    freelancersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerSkill", x => new { x.SkillsId, x.freelancersId });
                    table.ForeignKey(
                        name: "FK_FreelancerSkill_Freelancers_freelancersId",
                        column: x => x.freelancersId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelancerSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_projectSkills_SkillId",
                table: "projectSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_jobSkills_SkillId",
                table: "jobSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerSkill_freelancersId",
                table: "FreelancerSkill",
                column: "freelancersId");

            migrationBuilder.AddForeignKey(
                name: "FK_jobSkills_Jobs_JobId",
                table: "jobSkills",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jobSkills_Skills_SkillId",
                table: "jobSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projectSkills_Projects_ProjectId",
                table: "projectSkills",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projectSkills_Skills_SkillId",
                table: "projectSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
