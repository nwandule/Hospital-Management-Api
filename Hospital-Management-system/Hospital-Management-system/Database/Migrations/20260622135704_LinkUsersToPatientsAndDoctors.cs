using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_system.Database.Migrations
{
    /// <inheritdoc />
    public partial class LinkUsersToPatientsAndDoctors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsAvailable", "PhoneNumber", "Specialization", "UserId" },
                values: new object[] { 1, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doctor@hospital.com", "Dr. Medical Professional", true, "+27821112222", "General Medicine", 40 });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "UserId" },
                values: new object[] { "patient.test@gmail.com", 50 });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Users_UserId",
                table: "Doctors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Users_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Users_UserId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Users_UserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_UserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Doctors");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "johndoe@example.com");

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "Email", "FullName", "PhoneNumber" },
                values: new object[] { 2, "456 Hospital Road, Polokwane", "janesmith@example.com", "Jane Smith", "+27987654321" });
        }
    }
}
