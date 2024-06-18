using GigaHouse.Infrastructure.Models;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectViewModel>> GetProjects();

        Task<ProjectViewModel> GetProjectById(int id);

        Task<bool> IsExists(string key, string value);

        Task<bool> IsExistsForUpdate(int id, string key, string value);

        Task<ProjectViewModel> Create(ProjectViewModel model);

        Task Update(ProjectViewModel model);

        Task Delete(int id);
    }
}
