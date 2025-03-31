using GigaHouse.Infrastructure.Interfaces.Events;

namespace GigaHouse.Infrastructure.Events.ProductMedia
{
    public record ProductMediaCreatedEvent(Guid Id, string ProductMedia, DateTime CreatedAt) : IEvent;

    public record ProductMediaUpdatedEvent(Guid Id, string ProductMedia, DateTime CreatedAt) : IEvent;

    public record ProductMediaDeletedEvent(Guid Id, DateTime CreatedAt) : IEvent;
}
