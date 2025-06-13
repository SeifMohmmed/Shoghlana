using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class EnhanceJobManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Freelancers_FreelancerId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_FreelancerId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "FreelancerId",
                table: "Jobs",
                newName: "ProposalsCount");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Skills",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Skills",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Projects",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillId1",
                table: "JobSkills",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Jobs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Jobs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AcceptedFreelancerId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApproveTime",
                table: "Jobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeadLine",
                table: "Jobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationInDays",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Freelancers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Overview",
                table: "Freelancers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Freelancers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Freelancers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FreelancerNotifications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FreelancerNotifications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ClientNotifications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ClientNotifications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 1, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 2, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 3, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 4, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 5, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 6, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 7, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 8, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 9, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { 10, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "AcceptedFreelancerId", "ApproveTime", "DeadLine", "DurationInDays", "ProposalsCount" },
                values: new object[] { null, null, null, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimePublished",
                value: new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimePublished",
                value: new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_SkillId1",
                table: "JobSkills",
                column: "SkillId1");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_AcceptedFreelancerId",
                table: "Jobs",
                column: "AcceptedFreelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Freelancers_AcceptedFreelancerId",
                table: "Jobs",
                column: "AcceptedFreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkills_Skills_SkillId1",
                table: "JobSkills",
                column: "SkillId1",
                principalTable: "Skills",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Freelancers_AcceptedFreelancerId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkills_Skills_SkillId1",
                table: "JobSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobSkills_SkillId1",
                table: "JobSkills");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_AcceptedFreelancerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "SkillId1",
                table: "JobSkills");

            migrationBuilder.DropColumn(
                name: "AcceptedFreelancerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ApproveTime",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DeadLine",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "ProposalsCount",
                table: "Jobs",
                newName: "FreelancerId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Overview",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FreelancerNotifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FreelancerNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ClientNotifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ClientNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "FreelancerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "FreelancerId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "FreelancerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 4,
                column: "FreelancerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 5,
                column: "FreelancerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 6,
                column: "FreelancerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 7,
                column: "FreelancerId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 8,
                column: "FreelancerId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 9,
                column: "FreelancerId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 10,
                column: "FreelancerId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 11,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 12,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 13,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 14,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 15,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 16,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 17,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 18,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 19,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 20,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 21,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 22,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 23,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 24,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 25,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 26,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 27,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 28,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 29,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 30,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 31,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 32,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 33,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 34,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 35,
                column: "FreelancerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimePublished",
                value: null);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimePublished",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_FreelancerId",
                table: "Jobs",
                column: "FreelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Freelancers_FreelancerId",
                table: "Jobs",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id");
        }
    }
}
