using AutoMapper;
using GigaHouse.Data.Context;
using GigaHouse.Infrastructure.Interfaces.Repositories;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Mapping;
using GigaHouse.Infrastructure.Repositories;
using GigaHouse.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLog.LogManager.GetCurrentClassLogger();

try
{
    logger.Info("Init app");

    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddControllers();

    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var environmentConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_CONNECTIONSTRING");  

    builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(environmentConnectionString, ServerVersion.AutoDetect(environmentConnectionString)));

    var mapperConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new ProjectProfile());
        mc.AddProfile(new TaskProfile());
    });

    IMapper mapper = mapperConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);

    builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
    builder.Services.AddScoped<ITaskRepository, TaskRepository>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<ITaskService, TaskService>();

    builder.Services.AddMemoryCache();
    builder.Services.AddLogging();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Erro ao aplicar migrações do banco de dados.");
            throw;
        }
    }

    var mapperApp = app.Services.GetRequiredService<IMapper>();
    mapperApp.ConfigurationProvider.AssertConfigurationIsValid();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();    

    app.Run();
}
catch (Exception error)
{
    logger.Error(error, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
