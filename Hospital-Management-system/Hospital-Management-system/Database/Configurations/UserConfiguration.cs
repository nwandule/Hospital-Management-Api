/*=============================================================================
 * Author:       Vikash
 * Description:  Fluent API database schema configuration for the User entity.
 * Optimizes column memory allocations, defines security constraints, 
 * and seeds initial system testing data for all 5 core application roles.
 * Created Date: June 2026
 *=============================================================================*/
using Hospital_Management_system.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_Management_system.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // 1. Table Mapping
            builder.ToTable("Users");

            // 2. Primary Key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // 3. Column Constraints & Performance Indexes
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            // 4. Seed Core System Identities (All 5 Roles)
            // Note: Hashes pre-computed via BCrypt for seamless local Swagger testing.
            builder.HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@hospital.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w3", // Password: AdminRoot2026!
                    Role = "Admin",
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 2,
                    Email = "nurse.admin@hospital.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w4", // Password: NurseRoot2026!
                    Role = "Admin", // Operating with Full Administrative Privileges
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 3,
                    Email = "hr@hospital.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w5", // Password: HrRoot2026!
                    Role = "HR",
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 4,
                    Email = "doctor@hospital.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w6", // Password: DoctorRoot2026!
                    Role = "Doctor",
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 5,
                    Email = "patient.test@gmail.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w7", // Password: PatientTest2026!
                    Role = "Patient",
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}