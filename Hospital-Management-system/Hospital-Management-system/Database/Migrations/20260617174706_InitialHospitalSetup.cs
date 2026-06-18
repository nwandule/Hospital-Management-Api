using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Management_system.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialHospitalSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "Email", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Bank Street, Johannesburg", "johndoe@example.com", "John Doe", "+27123456789" },
                    { 2, "456 Hospital Road, Polokwane", "janesmith@example.com", "Jane Smith", "+27987654321" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 10, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@hospital.com", "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w3", "Admin" },
                    { 20, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nurse.admin@hospital.com", "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w4", "Admin" },
                    { 30, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hr@hospital.com", "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w5", "HR" },
                    { 40, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doctor@hospital.com", "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w6", "Doctor" },
                    { 50, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "patient.test@gmail.com", "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w7", "Patient" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Email",
                table: "Doctors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
