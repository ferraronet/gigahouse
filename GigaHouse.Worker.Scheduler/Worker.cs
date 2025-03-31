using GigaHouse.Infrastructure.Events.Task;
using GigaHouse.Infrastructure.Interfaces.Events;
using GigaHouse.Infrastructure.Interfaces.Services;
using Newtonsoft.Json;

namespace GigaHouse.Worker.Scheduler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var taskService = scope.ServiceProvider.GetRequiredService<ITaskService>();

                    await taskService.ProcessTasksAsync();
                }

                await Task.Delay(300000, stoppingToken);
            }
        }
    }
}
