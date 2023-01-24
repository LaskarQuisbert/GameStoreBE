using GameStore.Data;
using GameStore.Models;

namespace GameStore.Repositories
{
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        private readonly GameStoreDbContext _db;

        public ProductRepository(GameStoreDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
