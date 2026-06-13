/*=============================================================================
 * Author:       Vikash
 * Description:  Generic Data Access Repository Interface contract. Defines 
 * foundational, reusable CRUD operations shared across all 
 * domain entity workflows.
 * Created Date: June 2026
 *=============================================================================*/
namespace Hospital_Management_system.Services.CommonRepository
{
    public interface ICommonRepository<T> where T : class
    {
        T GetById(int id);
        List<T> GetAll();
        T Add(T entity);
    }
}
