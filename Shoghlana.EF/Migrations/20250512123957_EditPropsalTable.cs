using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class EditPropsalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Step 1: Prepare for column modification
            migrationBuilder.Sql(@"
                DECLARE @var1 sysname;
                SELECT @var1 = [d].[name]
                FROM [sys].[default_constraints] [d]
                INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
                WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Proposals]') AND [c].[name] = N'Deadline');
        
                IF @var1 IS NOT NULL 
                    EXEC(N'ALTER TABLE [Proposals] DROP CONSTRAINT [' + @var1 + ']');
            ");

            // Step 2: Add a new datetime2 column
            migrationBuilder.AddColumn<DateTime>(
                name: "NewDeadline",
                table: "Proposals",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            // Step 3: Update the new column with converted values
            migrationBuilder.Sql(@"
                UPDATE [Proposals] 
                SET [NewDeadline] = DATEADD(day, CAST([Deadline] AS int), '1900-01-01')
                WHERE [Deadline] IS NOT NULL
            ");

            // Step 4: Drop the old float column
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Proposals");

            // Step 5: Rename the new column
            migrationBuilder.RenameColumn(
                name: "NewDeadline",
                table: "Proposals",
                newName: "Deadline");

            // Step 6: Add Duration column
            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "Proposals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            // Step 7: Drop the Title column
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Proposals");

            // Step 8: Update seed data
            migrationBuilder.UpdateData(
                table: "Proposals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Deadline", "Duration" },
            values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });

            migrationBuilder.UpdateData(
                table: "Proposals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Deadline", "Duration" },
            values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0 });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert process
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Proposals");

            migrationBuilder.AlterColumn<double>(
                name: "Deadline",
                table: "Proposals",
                type: "float",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Proposals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Deadline", "Title" },
                values: new object[] { 0.0, null });

            migrationBuilder.UpdateData(
                table: "Proposals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Deadline", "Title" },
                values: new object[] { 0.0, null });
        }
    }
}
