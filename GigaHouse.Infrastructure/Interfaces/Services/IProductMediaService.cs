using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Models;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface IProductMediaService
    {
        Task<ProductMedia> CreateAsync(ProductMedia item, CancellationToken cancellationToken = default);

        Task<ProductMedia?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PaginatedList<ProductMedia>> GetAllByProductId(Guid productId, CancellationToken cancellationToken = default);
    }
}
