using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeadlinePropertyFromJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadLine",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ProposalsCount",
                table: "Jobs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeadLine",
                table: "Jobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProposalsCount",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DeadLine", "ProposalsCount" },
                values: new object[] { null, 0 });
        }
    }
}
