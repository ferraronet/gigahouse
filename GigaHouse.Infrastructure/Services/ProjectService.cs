using AutoMapper;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Models;

namespace GigaHouse.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectViewModel> Create(ProjectViewModel model)
        {
            var project = await _projectRepository.CreateAsync(_mapper.Map<Project>(model));
            return _mapper.Map<ProjectViewModel>(project);
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project != null)
                await _projectRepository.DeleteAsync(project);
        }

        public async Task<ProjectViewModel> GetProjectById(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectViewModel>(project);
        }

        public async Task<IEnumerable<ProjectViewModel>> GetProjects()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectViewModel>>(projects);
        }

        public async Task<bool> IsExists(string key, string value)
        {
            return await _projectRepository.IsExistsAsync(key, value);
        }

        public async Task<bool> IsExistsForUpdate(int id, string key, string value)
        {
            return await _projectRepository.IsExistsForUpdateAsync(id, key, value);
        }

        public async System.Threading.Tasks.Task Update(ProjectViewModel model)
        {
            var project = await _projectRepository.GetByIdAsync(model.Id);

            if (project != null)
            {
                project.Name = model.Name;
                project.UpdatedAt = model.UpdatedAt;

                await _projectRepository.UpdateAsync(project);
            }
        }
    }
}
