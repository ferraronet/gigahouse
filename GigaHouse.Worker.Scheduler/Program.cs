using GigaHouse.Core.Common.Settings;
using GigaHouse.Worker.Scheduler;
using GigaHouse.Data.Context;
using GigaHouse.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using GigaHouse.Infrastructure.RabbitMQ;
using GigaHouse.Infrastructure.Mapping;
using GigaHouse.Infrastructure.HandlersScraping;



try
{
    var host = Host.CreateDefaultBuilder(args)
         .ConfigureServices((hostContext, services) =>
         {
             services.Configure<MongoDbSettings>(hostContext.Configuration.GetSection("MongoDbSettings"));
             services.RegisterDependencies();
             services.AddAutoMapper(typeof(Program).Assembly); //, typeof(MappingLayers).Assembly, typeof(HandlersScraping).Assembly
             services.AddMediatR(cfg =>
             {
                 cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
             });
             services.AddRebusMessagingScheduler(hostContext.Configuration);
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
