using GigaHouse.Core.Models;
using GigaHouse.Infrastructure.Models;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskViewModel>> GetTasks(int projectId);

        Task<PaginatedDataViewModel<TaskViewModel>> GetPaginatedTasks(int pageNumber, int pageSize);

        Task<TaskViewModel> GetTaskById(int id);

        Task<bool> IsExists(string key, string value, int id);

        Task<bool> IsExistsForUpdate(int id, string key, string value);

        Task<bool> IsExistsForUpdate(int id, string key, string value, int projectId);

        Task<TaskViewModel> Create(TaskViewModel model);

        Task Update(TaskViewModel model);

        Task Delete(int id);
    }
}
