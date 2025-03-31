using Microsoft.Extensions.DependencyInjection;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Services;
using GigaHouse.Infrastructure.Interfaces.Factories;
using GigaHouse.Infrastructure.Factories;

namespace GigaHouse.Infrastructure.ModuleInitializers;

public class WorkerModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        //services.AddScoped<IScraperService, ScraperService>();
        //services.AddScoped<ISeleniumDriverFactory, SeleniumDriverFactory>();

        //services.AddScoped<IHandleMessages<ProductMediaCreatedEvent>, ProductMediaCreatedEventHandler>();
        //services.AddScoped<IHandleMessages<ProductMediaUpdatedEvent>, ProductMediaUpdatedEventHandler>();
        //services.AddScoped<IHandleMessages<ProductMediaDeletedEvent>, ProductMediaDeletedEventHandler>();

    }
}