using GigaHouse.Infrastructure.Events.ProductMedia;
using GigaHouse.Infrastructure.HandlersLayer.ProductMedias;
using Rebus.Bus;

namespace GigaHouse.Worker.WebAPI
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker finalizando...");
            await base.StopAsync(cancellationToken);
        }
    }
}
