using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Models;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project> CreateAsync(Project item, CancellationToken cancellationToken = default);

        Task<Project?> UpdateAsync(Project item, CancellationToken cancellationToken = default);

        Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Project?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PaginatedList<Project>> GetAllProjects(int pageNumber = 1, int pageSize = 10, string? name = null, ProjectStatus? status = null, CancellationToken cancellationToken = default);
    }
}
