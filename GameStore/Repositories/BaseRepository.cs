using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        internal GameStoreDbContext _context;
        internal DbSet<T> _entities;

        public BaseRepository(GameStoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            try 
            {
                return await _entities.SingleOrDefaultAsync(obj => obj.Id.Equals(id));
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"Id given is invalid, {0}", ex);
            }
        }

        public async Task<T> UpsertAsync(T entity)
        {
            try
            {
                var old = _entities.Local.FirstOrDefault(e => e.Id.Equals(entity.Id));
                if (old != null)
                {
                    _context.Entry(old).State = EntityState.Detached;
                    _entities.Update(entity);
                }
                else
                    await _entities.AddAsync(entity);

                await SaveChangesAsync();
                return entity;
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        
        public async Task<bool> Delete(T entityToDelete)
        {
            try 
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    _entities.Attach(entityToDelete);
                }
                _entities.Remove(entityToDelete);
                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}
