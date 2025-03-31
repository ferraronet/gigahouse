using AutoMapper;
using GigaHouse.Infrastructure.Events.Task;
using GigaHouse.Infrastructure.Interfaces.Events;
using GigaHouse.Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rebus.Bus;
using Rebus.Handlers;

namespace GigaHouse.Infrastructure.HandlersLayer.Tasks
{
    public class TaskCreatedEventHandler : IHandleMessages<TaskCreatedEvent>
    {
        private readonly ILogger<TaskCreatedEvent> _logger;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly IEventDispatcher _eventDispatcher;

        public TaskCreatedEventHandler(IMapper mapper, ITaskService taskService, ILogger<TaskCreatedEvent> logger, IEventDispatcher eventDispatcher)
        {
            _mapper = mapper;
            _taskService = taskService;
            _logger = logger;
            _eventDispatcher = eventDispatcher;
        }

        public async System.Threading.Tasks.Task Handle(TaskCreatedEvent message)
        {
            try
            {
                _logger.LogInformation($" [x] TaskCreatedEvent received: {message.Id}");

                if (message?.Task == null)
                {
                    _logger.LogWarning("TaskCreatedEvent received with null object.");
                    return;
                }

                try
                {
                    var item = JsonConvert.DeserializeObject<MessageTask>(message.Task.ToString());

                    if (item == null)
                    {
                        _logger.LogError("Failed to deserialize message from event message: Deserialization returned null.");
                        return;
                    }
                    else
                    {
                        await _taskService.CreateAsync(_mapper.Map<Data.Domain.Task>(item));
                        //await _eventDispatcher.PublishToWorkerWebScraping(new TaskScrapingEvent(message.Id, message.Task, message.CreatedAt));

                        _logger.LogInformation($" [✓] Processed ProductMedia Creation: {message.Id}");
                    }
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "Failed to deserialize message from event message: Invalid JSON format.");
                    return;
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error processing TaskCreatedEvent: {Id}", message.Id);
                throw;
            }
        }
    }
}
