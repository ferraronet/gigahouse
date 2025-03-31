namespace GigaHouse.Infrastructure.Interfaces.Events
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
    }
}
