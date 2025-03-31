//using AutoMapper;
//using GigaHouse.Data.Domain;
//using GigaHouse.Infrastructure.Interfaces.Repositories;
//using GigaHouse.Infrastructure.Mapping;
//using GigaHouse.Infrastructure.Models;
//using GigaHouse.Infrastructure.Services;
//using Moq;

//namespace GigaHouse.Test
//{
//    public class TaskServiceTests
//    {
//        private readonly Mock<IProjectRepository> _mockProjectRepository;
//        private readonly Mock<ITaskRepository> _mockTaskRepository;
//        private readonly IMapper _mapper;

//        public TaskServiceTests()
//        {
//            var mapperConfig = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile<TaskProfile>();
//            });
//            _mapper = mapperConfig.CreateMapper();

//            _mockProjectRepository = new Mock<IProjectRepository>();
//            _mockTaskRepository = new Mock<ITaskRepository>();
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task Create_ValidTaskViewModel_ReturnsCreatedViewModel()
//        {
//            var service = new TaskService(_mockProjectRepository.Object, _mockTaskRepository.Object, _mapper);
//            var taskViewModel = new TaskViewModel { ProjectId = 1, Name = "Test Task" };
//            var project = new Project { Id = 1, Name = "Test Project" };
//            var task = new Data.Domain.Task { Id = 1, ProjectId = 1, Name = "Test Task" };

//            _mockProjectRepository.Setup(repo => repo.GetByIdAsync(taskViewModel.ProjectId))
//                                  .ReturnsAsync(project);
//            _mockTaskRepository.Setup(repo => repo.CreateAsync(It.IsAny<Data.Domain.Task>()))
//                               .ReturnsAsync(task);

//            var result = await service.Create(taskViewModel);

//            Assert.Equal(taskViewModel.Name, result.Name);
//            Assert.Equal(task.Id, result.Id);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task Create_InvalidProjectId_ThrowsException()
//        {
//            var service = new TaskService(_mockProjectRepository.Object, _mockTaskRepository.Object, _mapper);
//            var taskViewModel = new TaskViewModel { ProjectId = 1, Name = "Test Task" };

//            _mockProjectRepository.Setup(repo => repo.GetByIdAsync(taskViewModel.ProjectId)).ReturnsAsync((Project)null);

//            await Assert.ThrowsAsync<Exception>(() => service.Create(taskViewModel));
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task Delete_ExistingTaskId_CallsRepositoryDeleteAsync()
//        {
//            var service = new TaskService(_mockProjectRepository.Object, _mockTaskRepository.Object, _mapper);
//            var taskId = 1;
//            var task = new Data.Domain.Task { Id = taskId, Name = "Test Task" };

//            _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskId))
//                               .ReturnsAsync(task);

//            await service.Delete(taskId);

//            _mockTaskRepository.Verify(repo => repo.DeleteAsync(task), Times.Once);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task GetTaskById_ExistingTaskId_ReturnsViewModel()
//        {
//            var service = new TaskService(_mockProjectRepository.Object, _mockTaskRepository.Object, _mapper);
//            var taskId = 1;
//            var task = new Data.Domain.Task { Id = taskId, Name = "Test Task" };

//            _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskId))
//                               .ReturnsAsync(task);

//            var result = await service.GetTaskById(taskId);

//            Assert.Equal(task.Name, result.Name);
//            Assert.Equal(task.Id, result.Id);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task GetTasks_ReturnsListOfViewModels()
//        {
//            var service = new TaskService(_mockProjectRepository.Object, _mockTaskRepository.Object, _mapper);
//            var projectId = 1;
//            var tasks = new List<Data.Domain.Task>
//        {
//            new Data.Domain.Task { Id = 1, ProjectId = projectId, Name = "Task 1" },
//            new Data.Domain.Task { Id = 2, ProjectId = projectId, Name = "Task 2" }
//        };

//            _mockTaskRepository.Setup(repo => repo.GetAllAsync())
//                               .ReturnsAsync(tasks);

//            var result = await service.GetTasks(projectId);

//            Assert.Equal(tasks.Count, result.Count());
//            Assert.Equal(tasks[0].Name, result.ElementAt(0).Name);
//            Assert.Equal(tasks[1].Name, result.ElementAt(1).Name);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task IsExists_KeyValueExists_ReturnsTrue()
//        {
//            var service = new TaskService(_mockProjectRepository.Object, _mockTaskRepository.Object, _mapper);
//            var key = "Name";
//            var value = "Test Task";
//            var id = 1;

//            _mockTaskRepository.Setup(repo => repo.IsExistsAsync(key, value, id, "ProjectId"))
//                               .ReturnsAsync(true);

//            var result = await service.IsExists(key, value, id);

//            Assert.True(result);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task IsExistsForUpdate_IdKeyValueExists_ReturnsTrue()
//        {
//            var service = new TaskService(_mockProjectRepository.Object, _mockTaskRepository.Object, _mapper);
//            var id = 1;
//            var key = "Name";
//            var value = "Test Task";

//            _mockTaskRepository.Setup(repo => repo.IsExistsForUpdateAsync(id, key, value))
//                               .ReturnsAsync(true);

//            var result = await service.IsExistsForUpdate(id, key, value);

//            Assert.True(result);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task IsExistsForUpdate_IdKeyValueProjectIdExists_ReturnsTrue()
//        {
//            var service = new TaskService(_mockProjectRepository.Object, _mockTaskRepository.Object, _mapper);
//            var id = 1;
//            var key = "Name";
//            var value = "Test Task";
//            var projectId = 1;

//            _mockTaskRepository.Setup(repo => repo.IsExistsForUpdateAsync(id, key, value, projectId, "ProjectId"))
//                               .ReturnsAsync(true);

//            var result = await service.IsExistsForUpdate(id, key, value, projectId);

//            Assert.True(result);
//        }

//        [Fact]
//        public async System.Threading.Tasks.Task Update_ExistingTaskViewModel_CallsRepositoryUpdateAsync()
//        {
//            var service = new TaskService(_mockProjectRepository.Object, _mockTaskRepository.Object, _mapper);
//            var taskViewModel = new TaskViewModel { Id = 1, Name = "Updated Task" };
//            var task = new Data.Domain.Task { Id = 1, Name = "Original Task" };

//            _mockTaskRepository.Setup(repo => repo.GetByIdAsync(taskViewModel.Id))
//                               .ReturnsAsync(task);

//            await service.Update(taskViewModel);

//            Assert.Equal(taskViewModel.Name, task.Name);
//            Assert.Equal(taskViewModel.UpdatedAt, task.UpdatedAt);
//            _mockTaskRepository.Verify(repo => repo.UpdateAsync(task), Times.Once);
//        }
//    }
//}
