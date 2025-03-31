using GigaHouse.Infrastructure.Interfaces.Events;

namespace GigaHouse.Infrastructure.Events.ProjectProduct
{
    public record ProjectProductCreatedEvent(Guid Id, object ProjectProduct, DateTime CreatedAt) : IEvent;

    public record ProjectProductUpdatedEvent(Guid Id, object ProjectProduct, DateTime CreatedAt) : IEvent;

    public record ProjectProductDeletedEvent(Guid Id, DateTime CreatedAt) : IEvent;
}
