using GigaHouse.Core.Common.Settings;
using GigaHouse.Infrastructure.Interfaces.Events;
using Microsoft.Extensions.Configuration;
using Rebus.Bus;

namespace GigaHouse.Infrastructure.RabbitMq
{
    public class RabbitMQEventDispatcher : IEventDispatcher
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public RabbitMQEventDispatcher(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        public async Task PublishToWorkerWebApi<T>(T @event) where T : IEvent
        {
            var settings = _configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();
            await _bus.Advanced.Routing.Send(settings.QueueWorkerApiName, @event);
            Console.WriteLine($" [x] Published event: {settings.QueueWebScrapingName} -> {@event}");
        }

        public async Task PublishToWorkerWebScraping<T>(T @event) where T : IEvent
        {
            var settings = _configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();
            await _bus.Advanced.Routing.Send(settings.QueueWebScrapingName, @event);
            Console.WriteLine($" [x] Published event: {settings.QueueWebScrapingName} -> {@event}");
        }
    }
}
