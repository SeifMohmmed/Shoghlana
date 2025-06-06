using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class RefactorNotificationsAndAdminTableStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Admins_AdminId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientNotifications_Notification_NotificationId",
                table: "ClientNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerNotifications_Notification_NotificationId",
                table: "FreelancerNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Categories_CategoryId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreelancerNotifications",
                table: "FreelancerNotifications");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerNotifications_NotificationId",
                table: "FreelancerNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientNotifications",
                table: "ClientNotifications");

            migrationBuilder.DropIndex(
                name: "IX_ClientNotifications_NotificationId",
                table: "ClientNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Admins",
                newName: "Admin");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "FreelancerNotifications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "ClientNotifications",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.DropColumn(
            name: "Id",
            table: "FreelancerNotifications");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FreelancerNotifications",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");


            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FreelancerNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SentTime",
                table: "FreelancerNotifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FreelancerNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ClientNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SentTime",
                table: "ClientNotifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ClientNotifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreelancerNotifications",
                table: "FreelancerNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientNotifications",
                table: "ClientNotifications",
                column: "ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                table: "Admin",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerNotifications_FreelancerId",
                table: "FreelancerNotifications",
                column: "FreelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Admin_AdminId",
                table: "AspNetUsers",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Category_CategoryId",
                table: "Jobs",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Admin_AdminId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Category_CategoryId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreelancerNotifications",
                table: "FreelancerNotifications");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerNotifications_FreelancerId",
                table: "FreelancerNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientNotifications",
                table: "ClientNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FreelancerNotifications");

            migrationBuilder.DropColumn(
                name: "SentTime",
                table: "FreelancerNotifications");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "FreelancerNotifications");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ClientNotifications");

            migrationBuilder.DropColumn(
                name: "SentTime",
                table: "ClientNotifications");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ClientNotifications");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Admin",
                newName: "Admins");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FreelancerNotifications",
                newName: "NotificationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ClientNotifications",
                newName: "NotificationId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NotificationId",
                table: "FreelancerNotifications",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreelancerNotifications",
                table: "FreelancerNotifications",
                columns: new[] { "FreelancerId", "NotificationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientNotifications",
                table: "ClientNotifications",
                columns: new[] { "ClientId", "NotificationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sentTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerNotifications_NotificationId",
                table: "FreelancerNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientNotifications_NotificationId",
                table: "ClientNotifications",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Admins_AdminId",
                table: "AspNetUsers",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientNotifications_Notification_NotificationId",
                table: "ClientNotifications",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerNotifications_Notification_NotificationId",
                table: "FreelancerNotifications",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Categories_CategoryId",
                table: "Jobs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
