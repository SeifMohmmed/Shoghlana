using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, null, "Category1" },
                    { 2, null, "Category2" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "FreelancerId", "Link", "TimePublished", "Title" },
                values: new object[,]
                {
                    { 1, "Description for Project1", 1, null, null, "Project1" },
                    { 2, "Description for Project2", 2, null, null, "Project2" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CategoryId", "ClientId", "Description", "ExperienceLevel", "FreelancerId", "MaxBudget", "MinBudget", "PostTime", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, "Description for Job1", 0, 1, 500m, 100m, new DateTime(2025, 5, 10, 16, 22, 37, 705, DateTimeKind.Unspecified), "Job1" },
                    { 2, 2, 2, "Description for Job2", 1, 2, 700m, 200m, new DateTime(2025, 5, 10, 16, 22, 37, 705, DateTimeKind.Unspecified), "Job2" }
                });

            migrationBuilder.InsertData(
                table: "Proposals",
                columns: new[] { "Id", "ApprovedTime", "Deadline", "Description", "FreelancerId", "JobId", "Price", "ReposLinks", "Status", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, null, 1, 1, 300m, null, 1, null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, null, 2, 2, 400m, null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Feedback", "JobId", "Value" },
                values: new object[,]
                {
                    { 1, null, 1, 4 },
                    { 2, null, 2, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Proposals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proposals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
