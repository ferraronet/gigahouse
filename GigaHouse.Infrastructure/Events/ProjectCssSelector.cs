using GigaHouse.Infrastructure.Interfaces.Events;

namespace GigaHouse.Infrastructure.Events.ProjectCssSelector
{
    public record ProjectCssSelectorCreatedEvent(Guid Id, object ProjectCssSelectorsEvents, DateTime CreatedAt) : IEvent;
                  
    public record ProjectCssSelectorUpdatedEvent(Guid Id, object ProjectCssSelectorsEvents, DateTime CreatedAt) : IEvent;
                  
    public record ProjectCssSelectorDeletedEvent(Guid Id, DateTime CreatedAt) : IEvent;
}
