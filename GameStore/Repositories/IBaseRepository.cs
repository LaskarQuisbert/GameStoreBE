using GameStore.Models;
using System.Linq.Expressions;

namespace GameStore.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<T> UpsertAsync(T entity);
        Task<bool> Delete(T entity);
        Task<bool> SaveChangesAsync();
    }
}
