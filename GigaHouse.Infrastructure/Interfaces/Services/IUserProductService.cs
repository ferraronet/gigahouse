using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;

namespace GigaHouse.Infrastructure.Interfaces.Services;

public interface IUserProductService
{
    Task<UserProduct> CreateAsync(UserProduct user, CancellationToken cancellationToken = default);


    Task<UserProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    Task<UserProduct?> GetByUserIdAndProductIdAndTaskIdAsync(Guid userId, Guid productId, Guid? taskId = default, CancellationToken cancellationToken = default);


    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PaginatedList<UserProduct>> GetAllByUserIdAsync(int pageNumber, int pageSize, Guid userId, CancellationToken cancellationToken = default);
}
