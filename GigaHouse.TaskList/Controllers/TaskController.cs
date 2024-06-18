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
    public class TaskController : ControllerBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ITaskService _taskService;
        private readonly IMemoryCache _memoryCache;

        public TaskController(ITaskService taskService, IMemoryCache memoryCache)
        {
            _taskService = taskService;
            _memoryCache = memoryCache;
        }

        [HttpGet("ByProjectId/{projectId}")]
        public async Task<IActionResult> GetByProjectId(int projectId)
        {
            var tasks = new List<TaskViewModel>();

            try
            {
                if (_memoryCache.TryGetValue($"Task_All", out List<TaskViewModel> cached))
                {
                    if (cached == null || cached.Count.Equals(0))
                        tasks = (await _taskService.GetTasks(projectId)).ToList();
                    else
                        tasks = cached;
                }
                else
                {
                    tasks = (await _taskService.GetTasks(projectId)).ToList();

                    if (tasks != null)
                        _memoryCache.Set($"Task_All", tasks, TimeSpan.FromMinutes(10));
                }

                return Ok(tasks);
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
            var task = new TaskViewModel();

            try
            {
                if (_memoryCache.TryGetValue($"Task_{id}", out TaskViewModel cached))
                {
                    task = cached;
                }
                else
                {
                    task = await _taskService.GetTaskById(id);

                    if (task != null)
                        _memoryCache.Set($"Task_{id}", task, TimeSpan.FromMinutes(10));
                }

                return Ok(task);
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] TaskSaveModel model)
        {
            try
            {
                if (!(await _taskService.IsExists("Name", model.Name, model.ProjectId)))
                {
                    var task = new TaskViewModel();
                    task.Name = model.Name;
                    task.Status = (int)Core.Enums.TaskStatus.ToDo;
                    task.CreatedAt = DateTime.Now;
                    task.UpdatedAt = DateTime.Now;
                    task.ProjectId = model.ProjectId;
                    task.Description = model.Description;

                    task = await _taskService.Create(task);

                    if (task != null)
                    {
                        _memoryCache.Set($"Task_{task.Id}", task, TimeSpan.FromMinutes(10));
                        _memoryCache.Remove($"Task_All");
                    }

                    return Ok(task);
                }
                else
                {
                    return StatusCode(500, new { message = "Task name already exists!" });
                }
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] TaskViewModel model)
        {
            try
            {
                if (!(await _taskService.IsExistsForUpdate(model.Id, "Name", model.Name, model.ProjectId)))
                {
                    var task = await _taskService.GetTaskById(model.Id);

                    if (task != null)
                    {
                        task.UpdatedAt = DateTime.Now;
                        task.Name = model.Name;
                        task.Description = model.Description;

                        await _taskService.Update(task);

                        _memoryCache.Set($"Task_{task.Id}", task, TimeSpan.FromMinutes(10));
                        _memoryCache.Remove($"Task_All");

                        return Ok(task);
                    }
                    else
                    {
                        return StatusCode(500, new { message = "Task not found" });
                    }
                }
                else
                {
                    return StatusCode(500, new { message = "Task name already exists" });
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
                await _taskService.Delete(id);

                _memoryCache.Remove($"Task_{id}");
                _memoryCache.Remove($"Task_All");

                return StatusCode(200, new { message = "Task deleted successfully" });

            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }
    }
}
