using GigaHouse.Core.Enums;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Events.Task;
using GigaHouse.Infrastructure.Interfaces.Events;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GigaHouse.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, IProductRepository productRepository, IEventDispatcher eventDispatcher)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _productRepository = productRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Data.Domain.Task> CreateAsync(Data.Domain.Task item, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            var project = await _projectRepository.GetByIdAsync(item.ProjectId);

            if (product != null && project != null)
            {
                var itemRepository = await _taskRepository.GetByIdAsync(item.Id);

                if (itemRepository == null)
                {
                    item.Product = product;
                    item.Project = project;
                    item.CreatedAt = DateTime.Now;
                    await _taskRepository.CreateAsync(item, cancellationToken);
                }
                else
                {
                    throw new Exception("Task already exists");
                }

                return item;
            }
            else
            {
                throw new Exception("Product or Project not exists");
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _taskRepository.GetByIdAsync(id);

            if (itemRepository != null)
                await _taskRepository.DeleteAsync(itemRepository);
            else
                return false;

            return true;
        }

        public async Task<Data.Domain.Task?> GetByProjectIdAndProductIdAndLinkAsync(Guid projectId, Guid productId, string link, CancellationToken cancellationToken = default)
        {
            var listItems = await _taskRepository.GetAllAsync();
            return listItems.FirstOrDefault(f => f.ProjectId == projectId && f.ProductId == productId && f.Link.Trim().ToUpper() == link.Trim().ToUpper());
        }

        public async Task<Data.Domain.Task?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _taskRepository.GetByIdAsync(id, f => f.Product, f => f.Project);
            return itemRepository;
        }

        public async Task<PaginatedList<Data.Domain.Task>> GetAllByProjectIdAndProductId(int pageNumber, int pageSize, Guid projectId, Guid productId, CancellationToken cancellationToken = default)
        {
            var projectProducts = await _taskRepository.GetPaginatedAsync(pageNumber, pageSize, filter: query =>
            {
                return query.Where(e => EF.Property<Guid>(e, "ProjectId") == projectId && EF.Property<Guid>(e, "ProductId") == productId);
            });

            return projectProducts;
        }

        public async Task<PaginatedList<Data.Domain.Task>> GetAllActiveTasksAsync(int pageNumber, int pageSize)
        {
            var query = @"SELECT * FROM ""Tasks"" WHERE ""Status"" = 1 AND (""LastDateSearch"" + INTERVAL '1 day' / CASE WHEN ""TimesPerDay"" = 0 THEN 1 ELSE ""TimesPerDay"" END) <= NOW()";

            var tasks = await _taskRepository.GetPaginatedBySqlQueryAsync<Data.Domain.Task>(query, pageNumber, pageSize);

            return tasks;
        }

        public async Task<Data.Domain.Task?> UpdateAsync(Data.Domain.Task item, CancellationToken cancellationToken = default)
        {
            var itemRepository = await _taskRepository.GetByIdAsync(item.Id);

            if (itemRepository != null)
            {
                itemRepository.Link = item.Link;
                itemRepository.TimesPerDay = item.TimesPerDay;
                itemRepository.Status = item.Status;
                itemRepository.UpdatedAt = DateTime.Now;

                await _taskRepository.UpdateAsync(itemRepository, cancellationToken);
            }

            return itemRepository;
        }

        public async System.Threading.Tasks.Task ProcessTasksAsync()
        {
            int pageNumber = 1;
            int pageSize = 10;
            bool hasPrevious = false;

            do
            {
                var list = await GetAllActiveTasksAsync(pageNumber, pageSize);

                foreach (var item in list)
                    await _eventDispatcher.PublishToWorkerWebScraping(new TaskScrapingEvent(item.Id, JsonConvert.SerializeObject(item), DateTime.Now));

                hasPrevious = list.HasPrevious;

                pageNumber++;

            } while (hasPrevious);
        }
    }
}
