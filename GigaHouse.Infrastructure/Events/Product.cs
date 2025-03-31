using GigaHouse.Infrastructure.Interfaces.Events;

namespace GigaHouse.Infrastructure.Events.Product
{
    public record ProductCreatedEvent(Guid Id, object Product, DateTime CreatedAt) : IEvent;

    public record ProductUpdatedEvent(Guid Id, object Product, DateTime CreatedAt) : IEvent;

    public record ProductDeletedEvent(Guid Id, DateTime CreatedAt) : IEvent;
}
