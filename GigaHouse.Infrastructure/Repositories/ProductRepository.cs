using GigaHouse.Core.Bases;
using GigaHouse.Data.Context;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;

namespace GigaHouse.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
