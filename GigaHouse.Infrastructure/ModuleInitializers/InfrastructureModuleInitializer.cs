//using GigaHouse.Data.Domain.Events.SaleEvents;
//using GigaHouse.Data.Domain.Persistence;
//using GigaHouse.Data.Domain.Repositories;
using GigaHouse.Infrastructure.Repositories;
//using GigaHouse.Data.Persistence;
//using GigaHouse.Data.Persistence.Handlers.Sales;
//using GigaHouse.Data.Persistence.MongoDB;
//using GigaHouse.Data.Persistence.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GigaHouse.Data.Context;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Services;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Interfaces.Events;
using GigaHouse.Infrastructure.RabbitMq;
using GigaHouse.Infrastructure.MongoDB;
using GigaHouse.Infrastructure.Factories;
using GigaHouse.Infrastructure.Interfaces.Factories;

namespace GigaHouse.Infrastructure.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<DbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectService, ProjectService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IProductMediaRepository, ProductMediaRepository>();
        services.AddScoped<IProductMediaService, ProductMediaService>();

        services.AddScoped<IProjectProductRepository, ProjectProductRepository>();
        services.AddScoped<IProjectProductService, ProjectProductService>();

        services.AddScoped<IProjectCssSelectorRepository, ProjectCssSelectorRepository>();
        services.AddScoped<IProjectCssSelectorService, ProjectCssSelectorService>();

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskService, TaskService>();

        services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();
        services.AddScoped<ITaskHistoryService, TaskHistoryService>();

        services.AddScoped<IUserProductRepository, UserProductRepository>();
        services.AddScoped<IUserProductService, UserProductService>();

        services.AddScoped<IRequestLogService, RequestLogService>();
        services.AddScoped<IEventDispatcher, RabbitMQEventDispatcher>();

        services.AddScoped<IScraperService, ScraperService>();
        services.AddScoped<ISeleniumDriverFactory, SeleniumDriverFactory>();
    }
}