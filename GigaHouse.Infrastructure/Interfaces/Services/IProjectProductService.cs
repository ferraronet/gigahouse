using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Models;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface IProjectProductService
    {
        Task<ProjectProduct> CreateAsync(ProjectProduct item, CancellationToken cancellationToken = default);

        Task<ProjectProduct?> UpdateAsync(ProjectProduct item, CancellationToken cancellationToken = default);

        Task<ProjectProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<ProjectProduct?> GetByProjectIdAndProductIdAsync(Guid projectId, Guid productId, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PaginatedList<ProjectProduct>> GetAllByProjectIdAndProductId(Guid projectId, Guid productId, CancellationToken cancellationToken = default);
    }
}
