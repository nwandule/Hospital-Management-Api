using Hospital_Management_system.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_Management_system.Database.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Specialization)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.IsAvailable)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasOne(d => d.User)
                .WithOne(u => u.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Doctor
                {
                    Id = 1,
                    FullName = "Dr. Medical Professional",
                    Email = "doctor@hospital.com",
                    Specialization = "General Medicine",
                    PhoneNumber = "+27821112222",
                    IsAvailable = true,
                    CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    UserId = 40
                }
            );
        }
    }
}