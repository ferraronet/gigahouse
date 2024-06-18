using AutoMapper;
using GigaHouse.Core.Interfaces;
using GigaHouse.Core.Models;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Models;
using GigaHouse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaHouse.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(IProjectRepository projectRepository, ITaskRepository taskRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskViewModel> Create(TaskViewModel model)
        {
            var project = await _projectRepository.GetByIdAsync(model.ProjectId);

            if (project != null)
            {
                var task = await _taskRepository.CreateAsync(_mapper.Map<Data.Domain.Task>(model));
                return _mapper.Map<TaskViewModel>(task);
            }
            else
            {
                throw new Exception("ProjectId not found");
            }
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task != null)
                await _taskRepository.DeleteAsync(task);
        }

        public Task<PaginatedDataViewModel<TaskViewModel>> GetPaginatedTasks(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskViewModel> GetTaskById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return _mapper.Map<TaskViewModel>(task);
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasks(int projectId)
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks.Where(f => f.ProjectId == projectId));
        }

        public async Task<bool> IsExists(string key, string value, int id)
        {
            return await _taskRepository.IsExistsAsync(key, value, id, "ProjectId");
        }

        public async Task<bool> IsExistsForUpdate(int id, string key, string value)
        {
            return await _taskRepository.IsExistsForUpdateAsync(id, key, value);
        }

        public async Task<bool> IsExistsForUpdate(int id, string key, string value, int projectId)
        {
            return await _taskRepository.IsExistsForUpdateAsync(id, key, value, projectId, "ProjectId");
        }

        public async System.Threading.Tasks.Task Update(TaskViewModel model)
        {
            var task = await _taskRepository.GetByIdAsync(model.Id);

            if (task != null)
            {
                task.Name = model.Name;
                task.UpdatedAt = model.UpdatedAt;
                task.Description = model.Description;

                await _taskRepository.UpdateAsync(task);
            }
        }
    }
}
