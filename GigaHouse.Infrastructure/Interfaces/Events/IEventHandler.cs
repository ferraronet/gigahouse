namespace GigaHouse.Infrastructure.Interfaces.Events
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task Handle(T message);
    }
}
