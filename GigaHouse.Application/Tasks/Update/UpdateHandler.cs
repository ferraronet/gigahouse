using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Services;

namespace GigaHouse.Application.Tasks.Update;

public class UpdateHandler : IRequestHandler<UpdateCommand, UpdateResult>
{
    private readonly IMapper _mapper;
    private readonly ITaskService _taskService;

    public UpdateHandler(IMapper mapper, ITaskService taskService)
    {
        _mapper = mapper;
        _taskService = taskService;
    }

    public async Task<UpdateResult> Handle(UpdateCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _taskService.GetByIdAsync(command.Id, cancellationToken);
        if (existingItem == null)
            throw new KeyNotFoundException($"Task with ID {command.Id} not exists");

        var updatedItem = await _taskService.UpdateAsync(_mapper.Map<Data.Domain.Task>(command), cancellationToken);
        var result = _mapper.Map<UpdateResult>(updatedItem);
        return result;
    }
}
