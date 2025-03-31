using GigaHouse.Core.Models;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface ITaskService
    {
        Task ProcessTasksAsync();

        Task<Data.Domain.Task> CreateAsync(Data.Domain.Task item, CancellationToken cancellationToken = default);

        Task<Data.Domain.Task?> UpdateAsync(Data.Domain.Task item, CancellationToken cancellationToken = default);

        Task<Data.Domain.Task?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Data.Domain.Task?> GetByProjectIdAndProductIdAndLinkAsync(Guid projectId, Guid productId, string link, CancellationToken cancellationToken = default);
        
        Task<PaginatedList<Data.Domain.Task>> GetAllActiveTasksAsync(int pageNumber, int pageSize);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PaginatedList<Data.Domain.Task>> GetAllByProjectIdAndProductId(int pageNumber, int pageSize, Guid projectId, Guid productId, CancellationToken cancellationToken = default);
    }
}
