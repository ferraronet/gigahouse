using AutoMapper;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Events.UserProduct;
using GigaHouse.Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rebus.Handlers;

namespace GigaHouse.Infrastructure.HandlersLayer.UserProducts
{
    public class UserProductCreatedEventHandler : IHandleMessages<UserProductCreatedEvent>
    {
        private readonly ILogger<UserProductCreatedEvent> _logger;
        private readonly IMapper _mapper;
        private readonly IUserProductService _userProductService;

        public UserProductCreatedEventHandler(IMapper mapper, IUserProductService userProductService, ILogger<UserProductCreatedEvent> logger)
        {
            _mapper = mapper;
            _userProductService = userProductService;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task Handle(UserProductCreatedEvent message)
        {
            try
            {
                _logger.LogInformation($" [x] UserProductCreatedEvent received: {message.Id}");

                if (message?.UserProduct == null)
                {
                    _logger.LogWarning("UserProductCreatedEvent received with null object.");
                    return;
                }

                try
                {
                    var item = JsonConvert.DeserializeObject<MessageUserProduct>(message.UserProduct.ToString());

                    if (item == null)
                    {
                        _logger.LogError("Failed to deserialize message from event message: Deserialization returned null.");
                        return;
                    }
                    else
                    {
                        await _userProductService.CreateAsync(_mapper.Map<UserProduct>(item));
                        _logger.LogInformation($" [✓] Processed UserProduct Creation: {message.Id}");
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
                _logger.LogError(error, "Error processing UserProductCreatedEvent: {Id}", message.Id);
                throw;
            }
        }
    }
}
