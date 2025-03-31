using GigaHouse.Infrastructure.Events.ProductMedia;
using GigaHouse.Infrastructure.Interfaces.Events;
using Rebus.Bus;
using Rebus.Sagas;

namespace GigaHouse.Infrastructure.HandlersLayer.ProductMedias
{
    public class CreateProductMediaSagaData : ISagaData, IEvent
    {
        public Guid Id { get; set; }

        public string ProductMedia { get; set; } = string.Empty;

        public int Revision { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class SagaHandlers : Saga<CreateProductMediaSagaData>, IAmInitiatedBy<ProductMediaCreatedEvent>
    {
        private readonly IBus _bus;

        public SagaHandlers(IBus bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<CreateProductMediaSagaData> config)
        {
            config.Correlate<ProductMediaCreatedEvent>(f => f.Id, s => s.Id);
        }

        public async System.Threading.Tasks.Task Handle(ProductMediaCreatedEvent message)
        {
            if (!IsNew)
                return;

            MarkAsComplete();
        }

    }
}
