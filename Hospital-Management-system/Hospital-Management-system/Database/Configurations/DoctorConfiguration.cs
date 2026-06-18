/*=============================================================================
 * Author:       Vikash
 * Description:  Fluent API database schema configuration for the Doctor entity.
 * Optimizes column memory allocations, defines string constraints, 
 * and maps the data structure cleanly to the physical SQL table structure.
 * Created Date: June 2026
 *=============================================================================*/
using Hospital_Management_system.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_Management_system.Database.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // 1. Table Mapping
            builder.ToTable("Doctors");

            // 2. Primary Key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // 3. Column Constraints & Performance Indexes
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            // Fast lookup index for searches/validation checking by email
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
        }
    }
}
