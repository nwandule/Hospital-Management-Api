using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Management_system.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedMultiRoleUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w3");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 2, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nurse.admin@hospital.com", "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w4", "Admin" },
                    { 3, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hr@hospital.com", "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w5", "HR" },
                    { 4, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doctor@hospital.com", "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w6", "Doctor" },
                    { 5, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "patient.test@gmail.com", "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w7", "Patient" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2b$12$MockHashPlaceholderForTestingPurposesOnly!!!");
        }
    }
}
