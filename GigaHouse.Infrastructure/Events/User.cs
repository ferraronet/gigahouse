using GigaHouse.Infrastructure.Interfaces.Events;

namespace GigaHouse.Infrastructure.Events.User
{
    public record UserCreatedEvent(Guid Id, object User, DateTime CreatedAt) : IEvent;
                  
    public record UserUpdatedEvent(Guid Id, object User, DateTime CreatedAt) : IEvent;
                  
    public record UserDeletedEvent(Guid Id, DateTime CreatedAt) : IEvent;
}
