using GigaHouse.Infrastructure.Events.ProductMedia;
using GigaHouse.Infrastructure.Events.Task;
using GigaHouse.Infrastructure.Interfaces.Events;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace GigaHouse.Infrastructure.HandlersLayer.Tasks
{
    public class CreateTaskSagaData : ISagaData, IEvent
    {
        public Guid Id { get; set; }

        public string Task { get; set; } = string.Empty;

        public int Revision { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool DoneTaskScrapingEvent { get; set; }
    }

    public class SagaHandlers : Saga<CreateTaskSagaData>, IAmInitiatedBy<TaskCreatedEvent>, IHandleMessages<TaskScrapingEvent>
    {
        private readonly IEventDispatcher _eventDispatcher;

        public SagaHandlers(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        protected override void CorrelateMessages(ICorrelationConfig<CreateTaskSagaData> config)
        {
            config.Correlate<TaskCreatedEvent>(f => f.Id, s => s.Id);
            config.Correlate<TaskScrapingEvent>(f => f.Id, s => s.Id);
        }

        public async Task Handle(TaskCreatedEvent message)
        {
            if (!IsNew)
                return;

            await _eventDispatcher.PublishToWorkerWebScraping(new TaskScrapingEvent(message.Id, message.Task, message.CreatedAt));
        }

        public Task Handle(TaskScrapingEvent message)
        {
            Data.DoneTaskScrapingEvent = true;

            MarkAsComplete();

            return Task.CompletedTask;
        }
    }
}
