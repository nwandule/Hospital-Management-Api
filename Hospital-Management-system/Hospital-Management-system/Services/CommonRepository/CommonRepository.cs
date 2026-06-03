/*=============================================================================
 * Author:       Vikash
 * Description:  Generic Data Access Repository Implementation. Leverages 
 * Entity Framework Core DbContext to handle database operations
 * generically for any registered domain model.
 * Created Date: June 2026
 *=============================================================================*/
using Hospital_Management_system.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_system.Services.CommonRepository
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); // Dynamically fetches the correct DbSet (e.g., Patients)
        }
        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges(); // Persists changes into SQL Server instantly
            return entity;
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            // Find searches the primary key directly in memory first, then queries the DB
            return _dbSet.Find(id);
        }
    }
}
