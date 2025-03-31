using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Events;
using Newtonsoft.Json;
using GigaHouse.Infrastructure.Events.UserProduct;

namespace GigaHouse.Application.UserProducts.Create;

public class CreateHandler : IRequestHandler<CreateCommand, CreateResult>
{
    private readonly IMapper _mapper;
    private readonly IEventDispatcher _eventDispatcher;

    public CreateHandler(IMapper mapper, IEventDispatcher eventDispatcher)
    {
        _mapper = mapper;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<CreateResult> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var item = _mapper.Map<UserProduct>(command);
        var id = Guid.NewGuid();
        item.Id = id;

        var eventDispatcher = new UserProductCreatedEvent(id, JsonConvert.SerializeObject(command), DateTime.Now);
        await _eventDispatcher.PublishToWorkerWebApi(eventDispatcher);

        var result = _mapper.Map<CreateResult>(item);
        return result;
    }
}
