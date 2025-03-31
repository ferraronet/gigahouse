using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Models;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface IProjectCssSelectorService
    {
        Task<ProjectCssSelector> CreateAsync(ProjectCssSelector item, CancellationToken cancellationToken = default);

        Task<ProjectCssSelector?> UpdateAsync(ProjectCssSelector item, CancellationToken cancellationToken = default);

        Task<ProjectCssSelector?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<ProjectCssSelector?> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<PaginatedList<ProjectCssSelector>> GetAllByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    }
}
