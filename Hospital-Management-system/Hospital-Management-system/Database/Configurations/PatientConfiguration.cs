/*=============================================================================
 * Author:       Vikash
 * Description:  Fluent API database schema configuration for the Patient entity.
 * Optimizes column memory allocations, defines constraints, 
 * and seeds initial system testing data.
 * Created Date: June 2026
 *=============================================================================*/
using Hospital_Management_system.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_Management_system.Database.Configurations
{
    public class PatientConfiguration: IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            // 1. Set Table Name (Optional - defaults to DbSet name)
            builder.ToTable("Patients");

            // 2. Set Primary Key Explicitly
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); // Auto-incrementing Identity column

            // 3. Optimize Column Data Sizes (Prevents wasting space!)
            // By default, EF Core creates NVARCHAR(MAX) which takes up massive space. 
            // Setting exact boundaries optimizes SQL indexing and storage memory.
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

            // 4. Seed Initial Data
            // This injects mock patients into the database table during the migration step.
            builder.HasData(
                new Patient
                {
                    Id = 1,
                    FullName = "John Doe",
                    Email = "johndoe@example.com",
                    PhoneNumber = "+27123456789",
                    Address = "123 Bank Street, Johannesburg"
                },
                new Patient
                {
                    Id = 2,
                    FullName = "Jane Smith",
                    Email = "janesmith@example.com",
                    PhoneNumber = "+27987654321",
                    Address = "456 Hospital Road, Polokwane"
                }
            );
        }
    }
}
