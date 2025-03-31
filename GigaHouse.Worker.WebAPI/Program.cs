using GigaHouse.Worker.WebAPI;
using GigaHouse.Core.Common.Settings;
using GigaHouse.Data.Context;
using GigaHouse.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using GigaHouse.Infrastructure.RabbitMQ;
using GigaHouse.Infrastructure.Mapping;
using GigaHouse.Infrastructure.HandlersLayer;
using GigaHouse.Infrastructure.Events;

try
{
    var host = Host.CreateDefaultBuilder(args)
         .ConfigureServices((hostContext, services) =>
         {
             services.Configure<MongoDbSettings>(hostContext.Configuration.GetSection("MongoDbSettings"));
             services.RegisterDependencies();
             services.AddAutoMapper(typeof(Program).Assembly, typeof(MappingLayers).Assembly, typeof(HandlersLayer).Assembly, typeof(EventsLayer).Assembly);
             services.AddMediatR(cfg =>
             {
                 cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
             });
             services.AddRebusMessagingWorker(hostContext.Configuration);
             services.AddDbContext<AppDbContext>(options => options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection")));

             services.AddHostedService<Worker>();

         }).Build();

    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

