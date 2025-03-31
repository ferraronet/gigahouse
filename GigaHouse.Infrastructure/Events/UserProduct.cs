using GigaHouse.Infrastructure.Interfaces.Events;

namespace GigaHouse.Infrastructure.Events.UserProduct
{
    public record UserProductCreatedEvent(Guid Id, string UserProduct, DateTime CreatedAt) : IEvent;
                  
    public record UserProductUpdatedEvent(Guid Id, string UserProduct, DateTime CreatedAt) : IEvent;
                  
    public record UserProductDeletedEvent(Guid Id, DateTime CreatedAt) : IEvent;
}
