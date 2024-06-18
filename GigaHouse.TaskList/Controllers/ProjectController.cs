using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Models;
using GigaHouse.TaskList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NLog;

namespace GigaHouse.TaskList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;
        private readonly IMemoryCache _memoryCache;

        public ProjectController(IProjectService projectService, ITaskService taskService, IMemoryCache memoryCache)
        {
            _projectService = projectService;
            _taskService = taskService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = new List<ProjectViewModel>();

            try
            {
                if (_memoryCache.TryGetValue($"Project_All", out List<ProjectViewModel> cached))
                {
                    if (cached == null || cached.Count.Equals(0))
                        projects = (await _projectService.GetProjects()).ToList();
                    else
                        projects = cached;
                }
                else
                {
                    projects = (await _projectService.GetProjects()).ToList();

                    if (projects != null)
                        _memoryCache.Set($"Project_All", projects, TimeSpan.FromMinutes(10));
                }

                return Ok(projects);
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = new ProjectViewModel();

            try
            {
                if (_memoryCache.TryGetValue($"Project_{id}", out ProjectViewModel cached))
                {
                    project = cached;
                }
                else
                {
                    project = await _projectService.GetProjectById(id);

                    if (project != null)
                        _memoryCache.Set($"Project_{id}", project, TimeSpan.FromMinutes(10));
                }

                return Ok(project);
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ProjectSaveModel model)
        {
            try
            {
                if (!(await _projectService.IsExists("Name", model.Name)))
                {
                    var project = new ProjectViewModel();
                    project.Name = model.Name;
                    project.Status = (int)Core.Enums.ProjectStatus.NotStarted;
                    project.CreatedAt = DateTime.Now;
                    project.UpdatedAt = DateTime.Now;

                    project = await _projectService.Create(project);

                    if (project != null)
                    {
                        _memoryCache.Set($"Project_{project.Id}", project, TimeSpan.FromMinutes(10));
                        _memoryCache.Remove($"Project_All");
                    }

                    return Ok(project);
                }
                else
                {
                    return StatusCode(500, new { message = "Project name already exists!" });
                }
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ProjectViewModel model)
        {
            try
            {
                if (!(await _projectService.IsExistsForUpdate(model.Id, "Name", model.Name)))
                {
                    var project = await _projectService.GetProjectById(model.Id);

                    if (project != null)
                    {
                        project.UpdatedAt = DateTime.Now;
                        project.Name = model.Name;

                        await _projectService.Update(project);

                        _memoryCache.Set($"Project_{project.Id}", project, TimeSpan.FromMinutes(10));
                        _memoryCache.Remove($"Project_All");

                        return Ok(project);
                    }
                    else
                    {
                        return StatusCode(500, new { message = "Project not found" });
                    }
                }
                else
                {
                    return StatusCode(500, new { message = "Project name already exists" });
                }
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var project = await _projectService.GetProjectById(id);

                if (project != null)
                {
                    var tasks = (await _taskService.GetTasks(id)).ToList();

                    foreach (var task in tasks)
                    {
                       _memoryCache.Remove($"Task_{task.Id}");
                    }

                    _memoryCache.Remove($"Task_All");
                }

                _memoryCache.Remove($"Project_{id}");
                _memoryCache.Remove($"Project_All");

                await _projectService.Delete(id);

                return StatusCode(200, new { message = "Project deleted successfully" });

            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }
    }
}
