using AutoMapper;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Events.ProductMedia;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.MongoDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rebus.Handlers;
using Serilog.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GigaHouse.Infrastructure.HandlersLayer.ProductMedias
{
    public class ProductMediaCreatedEventHandler : IHandleMessages<ProductMediaCreatedEvent>
    {
        private readonly ILogger<ProductMediaCreatedEvent> _logger;
        private readonly IMapper _mapper;
        private readonly IProductMediaService _productMediaService;

        public ProductMediaCreatedEventHandler(IMapper mapper, IProductMediaService productMediaService, ILogger<ProductMediaCreatedEvent> logger)
        {
            _mapper = mapper;
            _productMediaService = productMediaService;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task Handle(ProductMediaCreatedEvent message)
        {
            try
            {
                _logger.LogInformation($" [x] ProductMediaCreatedEvent received: {message.Id}");

                if (message?.ProductMedia == null)
                {
                    _logger.LogWarning("ProductMediaCreatedEvent received with null object.");
                    return;
                }

                try
                {
                    var item = JsonConvert.DeserializeObject<MessageProductMedia>(message.ProductMedia.ToString());

                    if (item == null)
                    {
                        _logger.LogError("Failed to deserialize message from event message: Deserialization returned null.");
                        return;
                    }
                    else
                    {
                        await _productMediaService.CreateAsync(_mapper.Map<ProductMedia>(item));
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
                _logger.LogError(error, "Error processing ProductMediaCreatedEvent: {Id}", message.Id);
                throw;
            }
        }
    }
}
