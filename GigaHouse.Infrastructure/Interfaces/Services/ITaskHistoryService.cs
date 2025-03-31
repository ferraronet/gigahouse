using GigaHouse.Data.Domain;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface ITaskHistoryService
    {
        Task<TaskHistory> CreateAsync(TaskHistory item, CancellationToken cancellationToken = default);
    }
}
