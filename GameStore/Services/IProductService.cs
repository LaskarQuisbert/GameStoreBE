using GameStore.Models;

namespace GameStore.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<Product> GetById(int Id);
        public Task<Product> SaveUpdate(Product value);
        public Task<bool> Delete(int id);

    }
}
