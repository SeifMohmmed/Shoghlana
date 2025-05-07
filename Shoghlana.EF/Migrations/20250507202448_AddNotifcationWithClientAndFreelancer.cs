using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddNotifcationWithClientAndFreelancer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Clients_ClientId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Freelancers_FreelancerId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_ClientId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_FreelancerId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "FreelancerId",
                table: "Notification");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedTime",
                table: "Proposals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Deadline",
                table: "Proposals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ReposLinks",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientNotifications",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientNotifications", x => new { x.ClientId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_ClientNotifications_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientNotifications_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FreelancerNotifications",
                columns: table => new
                {
                    FreelancerId = table.Column<int>(type: "int", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerNotifications", x => new { x.FreelancerId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_FreelancerNotifications_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelancerNotifications_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposalImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProposalId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalImages_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientNotifications_NotificationId",
                table: "ClientNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerNotifications_NotificationId",
                table: "FreelancerNotifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalImages_ProposalId",
                table: "ProposalImages",
                column: "ProposalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientNotifications");

            migrationBuilder.DropTable(
                name: "FreelancerNotifications");

            migrationBuilder.DropTable(
                name: "ProposalImages");

            migrationBuilder.DropColumn(
                name: "ApprovedTime",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "ReposLinks",
                table: "Proposals");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreelancerId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ClientId",
                table: "Notification",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_FreelancerId",
                table: "Notification",
                column: "FreelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Clients_ClientId",
                table: "Notification",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Freelancers_FreelancerId",
                table: "Notification",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id");
        }
    }
}
