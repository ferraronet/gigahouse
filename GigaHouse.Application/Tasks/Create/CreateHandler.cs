using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Interfaces.Events;
using Newtonsoft.Json;
using GigaHouse.Infrastructure.Events.Task;

namespace GigaHouse.Application.Tasks.Create;

public class CreateHandler : IRequestHandler<CreateCommand, CreateResult>
{
    private readonly IMapper _mapper;
    private readonly ITaskService _taskService;
    private readonly IEventDispatcher _eventDispatcher;

    public CreateHandler(IMapper mapper, ITaskService taskService, IEventDispatcher eventDispatcher)
    {
        _mapper = mapper;
        _taskService = taskService;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<CreateResult> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _taskService.GetByProjectIdAndProductIdAndLinkAsync(command.ProjectId, command.ProductId, command.Link, cancellationToken);
        if (existingItem != null)
            throw new InvalidOperationException($"Product with ID {command.ProductId} already exists");

        var item = _mapper.Map<Data.Domain.Task>(command);
        var id = Guid.NewGuid();
        item.Id = id;

        var eventDispatcher = new TaskCreatedEvent(id, JsonConvert.SerializeObject(command), DateTime.Now);
        await _eventDispatcher.PublishToWorkerWebApi(eventDispatcher);

        var result = _mapper.Map<CreateResult>(item);

        return result;
    }
}
