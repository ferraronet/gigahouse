using GigaHouse.Infrastructure.Interfaces.Events;

namespace GigaHouse.Infrastructure.Events.Task
{
    public record TaskCreatedEvent(Guid Id, string Task, DateTime CreatedAt) : IEvent;
                  
    public record TaskUpdatedEvent(Guid Id, string Task, DateTime CreatedAt) : IEvent;
                  
    public record TaskDeletedEvent(Guid Id, DateTime CreatedAt) : IEvent;

    public record TaskScrapingEvent(Guid Id, string Task, DateTime CreatedAt) : IEvent;
}
