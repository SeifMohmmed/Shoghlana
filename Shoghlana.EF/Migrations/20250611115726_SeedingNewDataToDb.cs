using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shoghlana.EF.Migrations
{
    /// <inheritdoc />
    public partial class SeedingNewDataToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Clients_ClientId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Jobs_JobId",
                table: "Proposals");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Proposals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinBudget",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxBudget",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Clients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Clients",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Includes all services related to graphic design, industrial design, and web design.", "Design Services" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Includes development and programming of applications and software for various systems and devices.", "Software Services" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 3, "Includes article writing, instant translation, and content creation for websites and blogs.", "Writing and Translation Services" },
                    { 4, "Includes managing digital marketing campaigns, social media advertising, and market analytics.", "Digital Marketing Services" },
                    { 5, "Includes user support, troubleshooting technical issues, and enhancing system and network performance.", "Technical Support Services" },
                    { 6, "Includes providing training courses, designing educational curricula, and developing learning resources.", "Education and Training Services" }
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Country", "Description", "Name", "Phone", "RegisterationTime" },
                values: new object[] { "Saudi Arabia", "A programmer and application developer specialized in web development.", "Abdulrahman Ahmed", "+966123456789", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Country", "Description", "Name", "Phone", "RegisterationTime" },
                values: new object[] { "Egypt", "A professional graphic designer specializing in logo and poster design.", "Fatima Mohammed", "+201234567890", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Country", "Description", "Image", "Name", "Phone", "RegisterationTime" },
                values: new object[,]
                {
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

            migrationBuilder.UpdateData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Name", "Overview", "Title" },
                values: new object[] { "Cairo, Egypt", "Mohamed Ahmed", "A professional developer with experience in web and mobile application development.", "Specialized Application Developer" });

            migrationBuilder.UpdateData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Name", "Overview", "Title" },
                values: new object[] { "Riyadh, Saudi Arabia", "Fatima Ali", "A highly experienced designer in logo and poster design.", "Professional Graphic Designer" });

            migrationBuilder.UpdateData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "Name", "Overview", "Title" },
                values: new object[] { "Cairo, Egypt", "Ahmed Khaled", "A programmer with experience in developing advanced applications using AI technologies.", "AI Specialist Programmer" });

            migrationBuilder.InsertData(
                table: "Freelancers",
                columns: new[] { "Id", "Address", "Name", "Overview", "PersonalImageBytes", "Title" },
                values: new object[,]
                {
                    { 4, "Dubai, United Arab Emirates", "Sarah Hussein", "A graphic designer with experience in abstract design and creative arts.", null, "Creative Abstract Designer" },
                    { 5, "Alexandria, Egypt", "Abdulrahman Mahmoud", "A professional developer with experience in building and developing large and complex websites.", null, "Advanced Web Developer" },
                    { 6, "Jeddah, Saudi Arabia", "Rima Abdullah", "A graphic designer with extensive experience in logo and brand identity design.", null, "Professional Graphic Designer" },
                    { 7, "Cairo, Egypt", "Mahmoud Ali", "A specialized developer with experience in mobile application development using the latest technologies.", null, "Mobile App Developer" },
                    { 8, "Riyadh, Saudi Arabia", "Noor Abdullah", "A developer with experience in advanced web and mobile application development.", null, "Professional App Developer" },
                    { 9, "Alexandria, Egypt", "Layla Mohammed", "A graphic designer and artist with experience in illustration and fine arts design.", null, "Creative Graphic Designer and Artist" },
                    { 10, "Manama, Bahrain", "Ali Al-Husseini", "A developer with experience in web and mobile application development using multiple languages.", null, "Electronic Application Developer" }
                });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Professional design and artistic work", "Professional and Unique Logo Design" });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "PostTime", "Title" },
                values: new object[] { 1, "Design and administrative artwork", new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Social Media Advertising Poster Design" });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Graphic Design");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Industrial Drawing");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Web Design");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "Brand Identity Design");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                column: "Title",
                value: "Product Design");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "Logo Design");

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
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
                columns: new[] { "Id", "CategoryId", "ClientId", "Description", "ExperienceLevel", "FreelancerId", "MaxBudget", "MinBudget", "PostTime", "Title" },
                values: new object[,]
                {
                    { 3, 1, 3, "Business card design", 2, 3, 600m, 150m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Professional Business Card Design for Printing" },
                    { 4, 2, 4, "Website and application development", 1, 4, 800m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Lifetime Free Control Panel Installation" },
                    { 5, 3, 5, "Website programming", 0, 5, 700m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Company Profile Website Design" },
                    { 6, 4, 6, "Programming and design of mobile apps", 2, 6, 3000m, 1000m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Mobile App Development for iOS and Android" },
                    { 7, 3, 7, "Programming and design of online shopping websites", 1, 7, 1500m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "E-Commerce Website Design and Development" },
                    { 8, 5, 8, "Marketing and advertising for companies and individuals", 0, 8, 1000m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Social Media Advertising Campaign Management" },
                    { 9, 6, 9, "Illustration and drawing arts", 1, 9, 600m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Illustration Design for Children's Books" },
                    { 10, 1, 10, "Marketing and advertising content writing", 0, 10, 300m, 100m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Advertising Content Writing for Website" },
                    { 11, 2, 11, "Advanced administrative systems programming", 2, null, 2000m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Employee Management System Design and Programming" },
                    { 12, 3, 12, "Economic and financial analysis", 2, null, 5000m, 1000m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Feasibility Study for Future Business Project" },
                    { 13, 4, 13, "Educational and training courses", 0, null, 200m, 50m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Online Programming Lessons for Beginners" },
                    { 14, 5, 14, "Graphic design and advertising", 1, null, 500m, 150m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Promotional Print Design for Cultural Event" },
                    { 15, 6, 15, "Translation and writing", 2, null, 800m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Translation of Scientific Articles from English to Arabic" },
                    { 16, 1, 16, "Video game programming", 2, null, 5000m, 1000m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Mobile Video Game Design and Development" },
                    { 17, 2, 17, "Design and development of e-learning platforms", 1, null, 1500m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Online Educational Platform Design" },
                    { 18, 3, 18, "Content writing and editing", 1, null, 700m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Content Management for Tech Blog" },
                    { 19, 4, 1, "CRM system programming and customization", 2, null, 2500m, 800m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "CRM System Design and Development" },
                    { 20, 5, 2, "Data analysis and report preparation", 1, null, 1000m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Data Analysis and Strategic Report Preparation for Companies" },
                    { 21, 6, 3, "Educational and research content writing", 2, null, 1500m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Writing and Editing E-Books on AI" },
                    { 22, 1, 4, "Programming and design of educational websites", 1, null, 1200m, 400m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Educational Website Design and Development for Students" },
                    { 23, 2, 5, "Design and programming of booking apps", 1, null, 1800m, 600m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Online Event Booking Platform Design and Programming" },
                    { 24, 3, 6, "Improving website search engine performance", 0, null, 800m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Website Search Engine Optimization (SEO)" },
                    { 25, 4, 7, "Integrated management systems programming", 2, null, 2500m, 700m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Inventory and Sales Management System for Small Businesses" },
                    { 26, 5, 8, "Economic and financial analysis for real estate projects", 2, null, 5000m, 1500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Feasibility Study for a New Residential Project" },
                    { 27, 6, 9, "Personal assistant app programming", 2, null, 3000m, 800m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Online Personal Assistant App Design and Development" },
                    { 28, 1, 10, "Marketing and fundraising", 1, null, 1500m, 400m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Create and Manage Online Fundraising Campaign" },
                    { 29, 2, 11, "Design and programming of interactive educational platforms", 1, null, 2000m, 600m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Interactive Educational Platform for Teaching Mathematics" },
                    { 30, 3, 12, "Educational game programming and design", 0, null, 1200m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Educational Video Game Design for Children" },
                    { 31, 4, 13, "Policy analysis and report preparation", 0, null, 700m, 200m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Research Report on Public Policy" },
                    { 32, 5, 14, "Content management systems programming and customization", 1, null, 1500m, 400m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Blog Content Management System Design and Programming" },
                    { 33, 6, 15, "Product marketing and advertising", 0, null, 1000m, 300m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Marketing Campaign for a New Product" },
                    { 34, 1, 16, "Project management system programming", 2, null, 2500m, 600m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Engineering Project Management System Design and Programming" },
                    { 35, 2, 17, "Educational apps programming and design", 1, null, 1800m, 500m, new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(615), "Programming Language Learning App Design and Development" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Clients_ClientId",
                table: "Jobs",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Jobs_JobId",
                table: "Proposals",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Clients_ClientId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Jobs_JobId",
                table: "Proposals");

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Proposals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinBudget",
                table: "Jobs",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxBudget",
                table: "Jobs",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Jobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { null, "Category1" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Title" },
                values: new object[] { null, "Category2" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Country", "Description", "Name", "Phone", "RegisterationTime" },
                values: new object[] { null, null, "Client1", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Country", "Description", "Name", "Phone", "RegisterationTime" },
                values: new object[] { null, null, "Client2", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Name", "Overview", "Title" },
                values: new object[] { null, "Ahmed Mohammed", null, "Backend Developer" });

            migrationBuilder.UpdateData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Name", "Overview", "Title" },
                values: new object[] { null, "Ali Suleiman", null, "Frontend Developer" });

            migrationBuilder.UpdateData(
                table: "Freelancers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "Name", "Overview", "Title" },
                values: new object[] { null, "Wael Abdul Rahim", null, "Backend Developer" });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Description for Job1", "Job1" });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "PostTime", "Title" },
                values: new object[] { 2, "Description for Job2", new DateTime(2025, 6, 8, 22, 9, 59, 384, DateTimeKind.Local).AddTicks(625), "Job2" });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "C#");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "LINQ");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "EF");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "OOP");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                column: "Title",
                value: "Agile");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "Blazor");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Clients_ClientId",
                table: "Jobs",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Jobs_JobId",
                table: "Proposals",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
