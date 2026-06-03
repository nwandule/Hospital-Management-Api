/*=============================================================================
 * Author:       Vikash
 * Description:  Application Database Context acting as the primary gateway 
 * between the domain models and the SQL Server instance. 
 * Manages the Change Tracker, database connections, and 
 * maps the Patient domain model to the physical SQL table.
 * Created Date: June 2026
 *=============================================================================*/
using Hospital_Management_system.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hospital_Management_system.Database.Entities
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
        }
        public DbSet<Patient> Patients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🟢 This single line automatically scans your whole project assembly 
            // and applies any configuration files inside your new Configurations folder!
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
    
}
