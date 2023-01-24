using GameStore.Models;
using GameStore.Models.Dtos;
using GameStore.Repositories;

namespace GameStore.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetById(int Id)
        {
            return await _productRepository.GetByIdAsync(Id);
        }

        public async Task<Product> SaveUpdate(Product value)
        {
            return await _productRepository.UpsertAsync(value);
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _productRepository.GetByIdAsync(id);
            return await _productRepository.Delete(obj);
        }
    }
}
