using GigaHouse.Infrastructure.Interfaces.Events;

namespace GigaHouse.Infrastructure.Events.Project
{
    public record ProjectCreatedEvent(Guid Id, object Project, DateTime CreatedAt) : IEvent;

    public record ProjectUpdatedEvent(Guid Id, object Project, DateTime CreatedAt) : IEvent;

    public record ProjectDeletedEvent(Guid Id, DateTime CreatedAt) : IEvent;
}
