using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Models;
using GigaHouse.Infrastructure.Models.Caches;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product item, CancellationToken cancellationToken = default);

        Task<Product?> UpdateAsync(Product item, CancellationToken cancellationToken = default);

        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Product?> GetByGtinAsync(string gtin, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PaginatedList<Product>> GetAllProducts(int pageNumber = 1, int pageSize = 10, string? gtin = null, string? name = null, ProductStatus? status = null, CancellationToken cancellationToken = default);

        Task<UserProductLog> GetProductPricesByUserIdAsync(Guid userId = default, Guid productId = default, CancellationToken cancellationToken = default);
    }
}
