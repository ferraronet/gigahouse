namespace GigaHouse.Infrastructure.Interfaces.Events
{
    public interface IEventDispatcher
    {
        Task PublishToWorkerWebApi<T>(T @event) where T : IEvent;

        Task PublishToWorkerWebScraping<T>(T @event) where T : IEvent;
    }
}
