//using AutoMapper;
//using GigaHouse.Infrastructure.Events.Task;
//using GigaHouse.Infrastructure.Interfaces.Services;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Rebus.Handlers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GigaHouse.WebScraping.Handlers.Tasks
//{
//    public class TaskScrapingEventHandler : IHandleMessages<TaskScrapingEvent>
//    {
//        private readonly ILogger<TaskScrapingEvent> _logger;
//        private readonly IMapper _mapper;
//        private readonly ITaskService _taskService;

//        public TaskScrapingEventHandler(IMapper mapper, ITaskService taskService, ILogger<TaskScrapingEvent> logger)
//        {
//            _mapper = mapper;
//            _taskService = taskService;
//            _logger = logger;
//        }

//        public async System.Threading.Tasks.Task Handle(TaskScrapingEvent message)
//        {
//            try
//            {
//                _logger.LogInformation($" [x] TaskScrapingEvent received: {message.Id}");

//                if (message?.Task == null)
//                {
//                    _logger.LogWarning("TaskScrapingEvent received with null object.");
//                    return;
//                }

//                try
//                {
//                    var item = JsonConvert.DeserializeObject<MessageTask>(message.Task.ToString());

//                    if (item == null)
//                    {
//                        _logger.LogError("Failed to deserialize message from event message: Deserialization returned null.");
//                        return;
//                    }
//                    else
//                    {
//                        //await _taskService.CreateAsync(_mapper.Map<Data.Domain.Task>(item));
//                        _logger.LogInformation($" [✓] Processed ProductMedia Creation: {message.Id}");
//                    }
//                }
//                catch (JsonException jsonEx)
//                {
//                    _logger.LogError(jsonEx, "Failed to deserialize message from event message: Invalid JSON format.");
//                    return;
//                }
//            }
//            catch (Exception error)
//            {
//                _logger.LogError(error, "Error processing TaskScrapingEvent: {Id}", message.Id);
//                throw;
//            }
//        }
//    }
//}
