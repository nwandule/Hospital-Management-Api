using Hospital_Management_system.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_Management_system.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

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

            builder.HasData(
                new User
                {
                    Id = 10,
                    Email = "admin@hospital.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w3",
                    Role = "Admin",
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 20,
                    Email = "nurse.admin@hospital.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w4",
                    Role = "Admin",
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 30,
                    Email = "hr@hospital.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w5",
                    Role = "HR",
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 40,
                    Email = "doctor@hospital.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w6",
                    Role = "Doctor",
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 50,
                    Email = "patient.test@gmail.com",
                    PasswordHash = "$2a$11$K7E6UfXwTz7zVbF3g3iOee9GzZf1gV2m3n4o5p6q7r8s9t0u1v2w7",
                    Role = "Patient",
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}