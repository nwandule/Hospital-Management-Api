/*=============================================================================
 * Author:       Vikash
 * Description:  Generic Data Access Repository Implementation. Leverages 
 * Entity Framework Core DbContext to handle database operations
 * generically for any registered domain model.
 * Created Date: June 2026
 *=============================================================================*/
using Hospital_Management_system.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital_Management_system.Services.CommonRepository
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public CommonRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
