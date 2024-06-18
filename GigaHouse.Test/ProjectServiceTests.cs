using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Mapping;
using GigaHouse.Infrastructure.Models;
using GigaHouse.Infrastructure.Services;
using Moq;
using Xunit;

namespace GigaHouse.Test
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _mockProjectRepository;
        private readonly IMapper _mapper;

        public ProjectServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProjectProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _mockProjectRepository = new Mock<IProjectRepository>();
        }

        [Fact]
        public async System.Threading.Tasks.Task Create_ValidProjectViewModel_ReturnsCreatedViewModel()
        {
            var service = new ProjectService(_mockProjectRepository.Object, _mapper);
            var projectViewModel = new ProjectViewModel { Name = "Test Project" };
            var project = new Project { Id = 1, Name = "Test Project" };

            _mockProjectRepository.Setup(repo => repo.CreateAsync(It.IsAny<Project>()))
                                  .ReturnsAsync(project);

            var result = await service.Create(projectViewModel);

            Assert.Equal(projectViewModel.Name, result.Name);
            Assert.Equal(project.Id, result.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_ExistingProjectId_CallsRepositoryDeleteAsync()
        {
            var service = new ProjectService(_mockProjectRepository.Object, _mapper);
            var projectId = 1;
            var project = new Project { Id = projectId, Name = "Test Project" };

            _mockProjectRepository.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);

            await service.Delete(projectId);

            _mockProjectRepository.Verify(repo => repo.DeleteAsync(project), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetProjectById_ExistingProjectId_ReturnsViewModel()
        {
            var service = new ProjectService(_mockProjectRepository.Object, _mapper);
            var projectId = 1;
            var project = new Project { Id = projectId, Name = "Test Project" };

            _mockProjectRepository.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);

            var result = await service.GetProjectById(projectId);

            Assert.Equal(project.Name, result.Name);
            Assert.Equal(project.Id, result.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetProjects_ReturnsListOfViewModels()
        {
            var service = new ProjectService(_mockProjectRepository.Object, _mapper);
            var projects = new List<Project>
        {
            new Project { Id = 1, Name = "Project 1" },
            new Project { Id = 2, Name = "Project 2" }
        };

            _mockProjectRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(projects);

            var result = await service.GetProjects();

            Assert.Equal(projects.Count, result.Count());
            Assert.Equal(projects[0].Name, result.ElementAt(0).Name);
            Assert.Equal(projects[1].Name, result.ElementAt(1).Name);
        }

        [Fact]
        public async System.Threading.Tasks.Task IsExists_KeyValueExists_ReturnsTrue()
        {
            var service = new ProjectService(_mockProjectRepository.Object, _mapper);
            var key = "Name";
            var value = "Test Project";

            _mockProjectRepository.Setup(repo => repo.IsExistsAsync(key, value)).ReturnsAsync(true);

            var result = await service.IsExists(key, value);

            Assert.True(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task IsExistsForUpdate_IdKeyValueExists_ReturnsTrue()
        {
            var service = new ProjectService(_mockProjectRepository.Object, _mapper);
            var id = 1;
            var key = "Name";
            var value = "Test Project";

            _mockProjectRepository.Setup(repo => repo.IsExistsForUpdateAsync(id, key, value)).ReturnsAsync(true);

            var result = await service.IsExistsForUpdate(id, key, value);

            Assert.True(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Update_ExistingProjectViewModel_CallsRepositoryUpdateAsync()
        {
            var service = new ProjectService(_mockProjectRepository.Object, _mapper);
            var projectViewModel = new ProjectViewModel { Id = 1, Name = "Updated Project" };
            var project = new Project { Id = 1, Name = "Original Project" };

            _mockProjectRepository.Setup(repo => repo.GetByIdAsync(projectViewModel.Id)).ReturnsAsync(project);

            await service.Update(projectViewModel);

            Assert.Equal(projectViewModel.Name, project.Name);
            Assert.Equal(projectViewModel.UpdatedAt, project.UpdatedAt);
            _mockProjectRepository.Verify(repo => repo.UpdateAsync(project), Times.Once);
        }
    }
}
