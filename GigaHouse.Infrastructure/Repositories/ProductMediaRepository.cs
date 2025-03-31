using GigaHouse.Core.Bases;
using GigaHouse.Data.Context;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;

namespace GigaHouse.Infrastructure.Repositories
{
    public class ProductMediaRepository : BaseRepository<ProductMedia>, IProductMediaRepository
    {
        public ProductMediaRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
