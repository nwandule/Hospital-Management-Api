using Hospital_Management_system.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_Management_system.Database.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(p => p.User)
                .WithOne(u => u.Patient)
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Patient
                {
                    Id = 1,
                    FullName = "John Doe",
                    Email = "patient.test@gmail.com",
                    PhoneNumber = "+27123456789",
                    Address = "123 Bank Street, Johannesburg",
                    UserId = 50
                }
            );
        }
    }
}