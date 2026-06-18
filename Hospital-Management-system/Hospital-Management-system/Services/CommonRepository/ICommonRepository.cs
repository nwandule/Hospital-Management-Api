/*=============================================================================
 * Author:       Vikash
 * Description:  Generic Data Access Repository Interface contract. Defines 
 * foundational, reusable CRUD operations shared across all 
 * domain entity workflows.
 * Created Date: June 2026
 *=============================================================================*/
using System.Linq.Expressions;

namespace Hospital_Management_system.Services.CommonRepository
{
    public interface ICommonRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
