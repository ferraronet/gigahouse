using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Events.Task;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface IScraperService
    {
        System.Threading.Tasks.Task ScrapeTaskScrapingEventAsync(TaskScrapingEvent message);
    }
}
