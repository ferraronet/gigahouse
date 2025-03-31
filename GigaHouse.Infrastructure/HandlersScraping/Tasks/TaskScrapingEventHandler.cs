using AutoMapper;
using GigaHouse.Infrastructure.Events.Task;
using GigaHouse.Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using System.Threading;

namespace GigaHouse.Infrastructure.HandlersScraping.Tasks
{
    public class TaskScrapingEventHandler : IHandleMessages<TaskScrapingEvent>
    {
        private readonly ILogger<TaskScrapingEvent> _logger;
        private readonly IScraperService _scraperService;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public TaskScrapingEventHandler(ILogger<TaskScrapingEvent> logger, IScraperService scraperService)
        {
            _logger = logger;
            _scraperService = scraperService;
        }

        public async Task Handle(TaskScrapingEvent message)
        {
            await _semaphore.WaitAsync();
            try
            {
                _logger.LogInformation($"Processando scraping para: {message.Id}");

                await _scraperService.ScrapeTaskScrapingEventAsync(message);

                await Task.Delay(1000);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
