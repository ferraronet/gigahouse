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
    public class TaskHistoryService : ITaskHistoryService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskHistoryRepository _taskHistoryRepository;

        public TaskHistoryService(ITaskRepository taskRepository, ITaskHistoryRepository taskHistoryRepository)
        {
            _taskRepository = taskRepository;
            _taskHistoryRepository = taskHistoryRepository;
        }

        public async Task<TaskHistory> CreateAsync(TaskHistory item, CancellationToken cancellationToken = default)
        {
            var task = await _taskRepository.GetByIdAsync(item.TaskId, f=> f.Product, f => f.Project);

            if (task != null)
            {
                item.Id = Guid.NewGuid();
                item.Task = task;
                item.Project = task.Project;
                await _taskHistoryRepository.CreateAsync(item, cancellationToken);

                return item;
            }
            else
            {
                throw new Exception("Task not exists");
            }
        }
    }
}
