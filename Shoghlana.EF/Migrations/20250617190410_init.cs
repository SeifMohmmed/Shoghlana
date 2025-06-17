using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegisterationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Freelancers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalImageBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Overview = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freelancers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientNotifications_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    FreelancerId = table.Column<int>(type: "int", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FreelancerNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreelancerId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreelancerNotifications_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApproveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MinBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AcceptedFreelancerId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Freelancers_AcceptedFreelancerId",
                        column: x => x.AcceptedFreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Poster = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TimePublished = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FreelancerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    SkillId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => new { x.JobId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_JobSkills_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkills_Skills_SkillId1",
                        column: x => x.SkillId1,
                        principalTable: "Skills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ReposLinks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreelancerId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_Freelancers_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Freelancers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proposals_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.CheckConstraint("CK_VALUE_RANGE", "[Value] BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "FK_Rates_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectImages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectSkills",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkills", x => new { x.SkillId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77b5a044-5c9e-495e-8442-76a816c17a66", "89dfb7b5-d529-40d9-92f6-808adb869512", "Admin", "ADMIN" },
                    { "82841029-737c-4234-9b74-64e448755ee4", "4535993f-2b05-49a0-a746-f2b830671da5", "Client", "CLIENT" },
                    { "8f072cb2-2b03-4fb0-b15a-e2168fdb5f48", "811a2238-0931-40ba-aedf-129fb1c8a22d", "Freelancer", "FREELANCER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Includes all services related to graphic design, industrial design, and web design.", "Design Services" },
                    { 2, "Includes development and programming of applications and software for various systems and devices.", "Software Services" },
                    { 3, "Includes article writing, instant translation, and content creation for websites and blogs.", "Writing and Translation Services" },
                    { 4, "Includes managing digital marketing campaigns, social media advertising, and market analytics.", "Digital Marketing Services" },
                    { 5, "Includes user support, troubleshooting technical issues, and enhancing system and network performance.", "Technical Support Services" },
                    { 6, "Includes providing training courses, designing educational curricula, and developing learning resources.", "Education and Training Services" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Country", "Description", "Image", "Name", "Phone", "RegisterationTime" },
                values: new object[,]
                {
                    { 1, "Saudi Arabia", "A programmer and application developer specialized in web development.", null, "Abdulrahman Ahmed", "+966123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Egypt", "A professional graphic designer specializing in logo and poster design.", null, "Fatima Mohammed", "+201234567890", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "United Arab Emirates", "A professional digital marketer with experience in managing social media ad campaigns.", null, "Ali Alabdullah", "+971123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Jordan", "A content writer specialized in creative writing and artistic articles.", null, "Maryam Hassan", "+962123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Iraq", "A photographer specializing in event and special occasion photography.", null, "Yousef Khalid", "+964123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Saudi Arabia", "A professional project manager in tech and software development.", null, "Lama Abdullah", "+966123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Egypt", "A creative content marketer working on promoting digital content for startups.", null, "Omar Ahmed", "+201234567890", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Lebanon", "A professional app developer working in mobile app development.", null, "Rana Mahmoud", "+961123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Jordan", "A marketing manager specialized in digital marketing strategies.", null, "Ahmed Ali", "+962123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Saudi Arabia", "An expert in designing and managing websites for small and medium businesses.", null, "Huda Saleh", "+966123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "United Arab Emirates", "A professional financial accountant working in financial reporting.", null, "Salma Abdullah", "+971123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Egypt", "An architect specialized in residential building design.", null, "Mohammed Hassan", "+201234567890", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "Iraq", "A doctor specialized in pediatrics and mental health.", null, "Zainab Abdullah", "+964123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "Lebanon", "A creative graphic designer working in commercial ad design.", null, "Ahmed Hussein", "+961123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "Jordan", "A professional translator specializing in medical and scientific text translation.", null, "Fatima Ali", "+962123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "Saudi Arabia", "A professional web developer in electronic application development.", null, "Abdullah Mahmoud", "+966123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "Egypt", "An architectural engineer specialized in industrial facility design.", null, "Reem Abdullah", "+201234567890", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "Lebanon", "A financial accountant with extensive experience in financial accounting.", null, "Omar Hassan", "+961123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Freelancers",
                columns: new[] { "Id", "Address", "Name", "Overview", "PersonalImageBytes", "Title" },
                values: new object[,]
                {
                    { 1, "Cairo, Egypt", "Mohamed Ahmed", "A professional developer with experience in web and mobile application development.", null, "Specialized Application Developer" },
                    { 2, "Riyadh, Saudi Arabia", "Fatima Ali", "A highly experienced designer in logo and poster design.", null, "Professional Graphic Designer" },
                    { 3, "Cairo, Egypt", "Ahmed Khaled", "A programmer with experience in developing advanced applications using AI technologies.", null, "AI Specialist Programmer" },
                    { 4, "Dubai, United Arab Emirates", "Sarah Hussein", "A graphic designer with experience in abstract design and creative arts.", null, "Creative Abstract Designer" },
                    { 5, "Alexandria, Egypt", "Abdulrahman Mahmoud", "A professional developer with experience in building and developing large and complex websites.", null, "Advanced Web Developer" },
                    { 6, "Jeddah, Saudi Arabia", "Rima Abdullah", "A graphic designer with extensive experience in logo and brand identity design.", null, "Professional Graphic Designer" },
                    { 7, "Cairo, Egypt", "Mahmoud Ali", "A specialized developer with experience in mobile application development using the latest technologies.", null, "Mobile App Developer" },
                    { 8, "Riyadh, Saudi Arabia", "Noor Abdullah", "A developer with experience in advanced web and mobile application development.", null, "Professional App Developer" },
                    { 9, "Alexandria, Egypt", "Layla Mohammed", "A graphic designer and artist with experience in illustration and fine arts design.", null, "Creative Graphic Designer and Artist" },
                    { 10, "Manama, Bahrain", "Ali Al-Husseini", "A developer with experience in web and mobile application development using multiple languages.", null, "Electronic Application Developer" }
                });

            migrationBuilder.InsertData(
                table: "ProjectImages",
                columns: new[] { "Id", "Image", "ProjectId" },
                values: new object[,]
                {
                    { 1, new byte[] { 32, 33, 34, 35 }, null },
                    { 2, new byte[] { 32, 33, 34, 35 }, null }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, null, "Graphic Design" },
                    { 2, null, "Industrial Drawing" },
                    { 3, null, "Web Design" },
                    { 4, null, "Brand Identity Design" },
                    { 5, null, "Product Design" },
                    { 6, null, "Logo Design" },
                    { 7, null, "Mobile App Development" },
                    { 8, null, "Web Development" },
                    { 9, null, "Game Development" },
                    { 10, null, "Computer Programming" },
                    { 11, null, "Content Writing" },
                    { 12, null, "Article Writing" },
                    { 13, null, "Translation" },
                    { 14, null, "Proofreading" },
                    { 15, null, "Technical Writing" },
                    { 16, null, "Digital Marketing" },
                    { 17, null, "Search Engine Optimization (SEO)" },
                    { 18, null, "Social Media Advertising" },
                    { 19, null, "Email Marketing" },
                    { 20, null, "Content Marketing" },
                    { 21, null, "Technical Support" },
                    { 22, null, "Network Administration" },
                    { 23, null, "System Maintenance" },
                    { 24, null, "Desktop Support" },
                    { 25, null, "Cloud Computing Services" },
                    { 26, null, "Educational Software Development" },
                    { 27, null, "Curriculum Design" },
                    { 28, null, "E-Learning Development" },
                    { 29, null, "Lesson Design" },
                    { 30, null, "Online Teaching" },
                    { 31, null, "Advertisement Design" },
                    { 32, null, "User Interface (UI) Design" },
                    { 33, null, "User Experience (UX)" },
                    { 34, null, "3D Modeling" },
                    { 35, null, "Character Design" },
                    { 36, null, "App Development with React.js" },
                    { 37, null, "App Development with Node.js" },
                    { 38, null, "App Development with Ruby on Rails" },
                    { 39, null, "App Development with SQL" },
                    { 40, null, "App Development with Django" },
                    { 41, null, "Legal Article Writing" },
                    { 42, null, "Creative Writing" },
                    { 43, null, "Legal Verification" },
                    { 44, null, "Localization" },
                    { 45, null, "Market Analysis" },
                    { 46, null, "Statistical Analysis" },
                    { 47, null, "Performance Marketing" },
                    { 48, null, "Affiliate Marketing" },
                    { 49, null, "Online Marketing" },
                    { 50, null, "Ad Campaign Management" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "AcceptedFreelancerId", "ApproveTime", "CategoryId", "ClientId", "Description", "DurationInDays", "ExperienceLevel", "MaxBudget", "MinBudget", "PostTime", "Title" },
                values: new object[,]
                {
                    { 1, 1, null, 1, 1, "Professional design and artistic work", 0, 0, 500m, 100m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Professional and Unique Logo Design" },
                    { 2, 2, null, 1, 2, "Design and administrative artwork", 0, 1, 700m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Social Media Advertising Poster Design" },
                    { 3, 3, null, 1, 3, "Business card design", 0, 2, 600m, 150m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Professional Business Card Design for Printing" },
                    { 4, 4, null, 2, 4, "Website and application development", 0, 1, 800m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Lifetime Free Control Panel Installation" },
                    { 5, 5, null, 3, 5, "Website programming", 0, 0, 700m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Company Profile Website Design" },
                    { 6, 6, null, 4, 6, "Programming and design of mobile apps", 0, 2, 3000m, 1000m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Mobile App Development for iOS and Android" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "AcceptedFreelancerId", "ApproveTime", "CategoryId", "ClientId", "Description", "DurationInDays", "ExperienceLevel", "MaxBudget", "MinBudget", "PostTime", "Status", "Title" },
                values: new object[,]
                {
                    { 7, 7, null, 3, 7, "Programming and design of online shopping websites", 0, 1, 1500m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 2, "E-Commerce Website Design and Development" },
                    { 8, 8, null, 5, 8, "Marketing and advertising for companies and individuals", 0, 0, 1000m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 2, "Social Media Advertising Campaign Management" },
                    { 9, 9, null, 6, 9, "Illustration and drawing arts", 0, 1, 600m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 2, "Illustration Design for Children's Books" },
                    { 10, 10, null, 1, 10, "Marketing and advertising content writing", 0, 0, 300m, 100m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 2, "Advertising Content Writing for Website" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "AcceptedFreelancerId", "ApproveTime", "CategoryId", "ClientId", "Description", "DurationInDays", "ExperienceLevel", "MaxBudget", "MinBudget", "PostTime", "Title" },
                values: new object[,]
                {
                    { 11, null, null, 2, 11, "Advanced administrative systems programming", 0, 2, 2000m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Employee Management System Design and Programming" },
                    { 12, null, null, 3, 12, "Economic and financial analysis", 0, 2, 5000m, 1000m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Feasibility Study for Future Business Project" },
                    { 13, null, null, 4, 13, "Educational and training courses", 0, 0, 200m, 50m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Online Programming Lessons for Beginners" },
                    { 14, null, null, 5, 14, "Graphic design and advertising", 0, 1, 500m, 150m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Promotional Print Design for Cultural Event" },
                    { 15, null, null, 6, 15, "Translation and writing", 0, 2, 800m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Translation of Scientific Articles from English to Arabic" },
                    { 16, null, null, 1, 16, "Video game programming", 0, 2, 5000m, 1000m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Mobile Video Game Design and Development" },
                    { 17, null, null, 2, 17, "Design and development of e-learning platforms", 0, 1, 1500m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Online Educational Platform Design" },
                    { 18, null, null, 3, 18, "Content writing and editing", 0, 1, 700m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Content Management for Tech Blog" },
                    { 19, null, null, 4, 1, "CRM system programming and customization", 0, 2, 2500m, 800m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "CRM System Design and Development" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "AcceptedFreelancerId", "ApproveTime", "CategoryId", "ClientId", "Description", "DurationInDays", "ExperienceLevel", "MaxBudget", "MinBudget", "PostTime", "Status", "Title" },
                values: new object[,]
                {
                    { 20, null, null, 5, 2, "Data analysis and report preparation", 0, 1, 1000m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Data Analysis and Strategic Report Preparation for Companies" },
                    { 21, null, null, 6, 3, "Educational and research content writing", 0, 2, 1500m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Writing and Editing E-Books on AI" },
                    { 22, null, null, 1, 4, "Programming and design of educational websites", 0, 1, 1200m, 400m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Educational Website Design and Development for Students" },
                    { 23, null, null, 2, 5, "Design and programming of booking apps", 0, 1, 1800m, 600m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Online Event Booking Platform Design and Programming" },
                    { 24, null, null, 3, 6, "Improving website search engine performance", 0, 0, 800m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Website Search Engine Optimization (SEO)" },
                    { 25, null, null, 4, 7, "Integrated management systems programming", 0, 2, 2500m, 700m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Inventory and Sales Management System for Small Businesses" },
                    { 26, null, null, 5, 8, "Economic and financial analysis for real estate projects", 0, 2, 5000m, 1500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Feasibility Study for a New Residential Project" },
                    { 27, null, null, 6, 9, "Personal assistant app programming", 0, 2, 3000m, 800m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Online Personal Assistant App Design and Development" },
                    { 28, null, null, 1, 10, "Marketing and fundraising", 0, 1, 1500m, 400m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Create and Manage Online Fundraising Campaign" },
                    { 29, null, null, 2, 11, "Design and programming of interactive educational platforms", 0, 1, 2000m, 600m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Interactive Educational Platform for Teaching Mathematics" },
                    { 30, null, null, 3, 12, "Educational game programming and design", 0, 0, 1200m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Educational Video Game Design for Children" },
                    { 31, null, null, 4, 13, "Policy analysis and report preparation", 0, 0, 700m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), 1, "Research Report on Public Policy" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "AcceptedFreelancerId", "ApproveTime", "CategoryId", "ClientId", "Description", "DurationInDays", "ExperienceLevel", "MaxBudget", "MinBudget", "PostTime", "Title" },
                values: new object[,]
                {
                    { 32, null, null, 5, 14, "Content management systems programming and customization", 0, 1, 1500m, 400m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Blog Content Management System Design and Programming" },
                    { 33, null, null, 6, 15, "Product marketing and advertising", 0, 0, 1000m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Marketing Campaign for a New Product" },
                    { 34, null, null, 1, 16, "Project management system programming", 0, 2, 2500m, 600m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Engineering Project Management System Design and Programming" },
                    { 35, null, null, 2, 17, "Educational apps programming and design", 0, 1, 1800m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Programming Language Learning App Design and Development" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "FreelancerId", "Link", "Poster", "TimePublished", "Title" },
                values: new object[,]
                {
                    { 1, "Description for Project1", 1, null, new byte[] { 32, 33, 34, 35 }, new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project1" },
                    { 2, "Description for Project2", 2, null, new byte[] { 32, 33, 34, 35 }, new DateTime(2025, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project2" }
                });

            migrationBuilder.InsertData(
                table: "Proposals",
                columns: new[] { "Id", "ApprovedTime", "Deadline", "Description", "Duration", "FreelancerId", "JobId", "Price", "ReposLinks", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.0, 1, 1, 300m, null, 1 },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.0, 2, 2, 400m, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Feedback", "JobId", "Value" },
                values: new object[,]
                {
                    { 1, null, 1, 4 },
                    { 2, null, 2, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdminId",
                table: "AspNetUsers",
                column: "AdminId",
                unique: true,
                filter: "[AdminId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientId",
                table: "AspNetUsers",
                column: "ClientId",
                unique: true,
                filter: "[ClientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FreelancerId",
                table: "AspNetUsers",
                column: "FreelancerId",
                unique: true,
                filter: "[FreelancerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClientNotifications_ClientId",
                table: "ClientNotifications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerNotifications_FreelancerId",
                table: "FreelancerNotifications",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerSkills_FreelancerId",
                table: "FreelancerSkills",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_AcceptedFreelancerId",
                table: "Jobs",
                column: "AcceptedFreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CategoryId",
                table: "Jobs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_ClientId",
                table: "Jobs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_SkillId",
                table: "JobSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_SkillId1",
                table: "JobSkills",
                column: "SkillId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_ProjectId",
                table: "ProjectImages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FreelancerId",
                table: "Projects",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkills_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalImages_ProposalId",
                table: "ProposalImages",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_FreelancerId",
                table: "Proposals",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_JobId",
                table: "Proposals",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_JobId",
                table: "Rates",
                column: "JobId",
                unique: true,
                filter: "[JobId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClientNotifications");

            migrationBuilder.DropTable(
                name: "FreelancerNotifications");

            migrationBuilder.DropTable(
                name: "FreelancerSkills");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.DropTable(
                name: "ProjectImages");

            migrationBuilder.DropTable(
                name: "ProjectSkills");

            migrationBuilder.DropTable(
                name: "ProposalImages");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Freelancers");
        }
    }
}
