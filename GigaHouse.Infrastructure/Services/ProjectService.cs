using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace GigaHouse.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> CreateAsync(Project item, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectRepository.GetByIdAsync(item.Id);

            if (itemRepository == null)
            {
                item.CreatedAt = DateTime.Now;
                await _projectRepository.CreateAsync(item, cancellationToken);
            }
            else
            {
                throw new Exception("Project already exists");
            }

            return item;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectRepository.GetByIdAsync(id);

            if (itemRepository != null)
                await _projectRepository.DeleteAsync(itemRepository);
            else
                return false;

            return true;
        }

        public async Task<Project?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var listItems = await _projectRepository.GetAllAsync();
            return listItems.FirstOrDefault(f => f.Name == name);
        }

        public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectRepository.GetByIdAsync(id);
            return itemRepository;
        }

        public async Task<PaginatedList<Project>> GetAllProjects(int pageNumber = 1, int pageSize = 10, string? name = null, ProjectStatus? status = null, CancellationToken cancellationToken = default)
        {
            var projects = await _projectRepository.GetPaginatedAsync(pageNumber: pageNumber, pageSize: pageSize, filter: query =>
            {
                if (!string.IsNullOrEmpty(name))
                    query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, "Name").Trim().ToUpper(), $"%{name.Trim().ToUpper()}%"));

                if (status != null)
                    query = query.Where(e => EF.Property<int>(e, "Status") == (int)status);

                return query;
            });

            return projects;
        }


        public async Task<Project?> UpdateAsync(Project item, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _projectRepository.GetByIdAsync(item.Id);

            if (itemRepository != null)
            {
                itemRepository.Name = item.Name;
                itemRepository.Link = item.Link;
                itemRepository.Status = item.Status;
                itemRepository.UpdatedAt = DateTime.Now;

                await _projectRepository.UpdateAsync(itemRepository, cancellationToken);
            }

            return itemRepository;
        }
    }
}
