using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;
using AutoMapper;

namespace GigaHouse.Application.Tasks.Delete;

public class DeleteHandler : IRequestHandler<DeleteCommand, DeleteResponse>
{
    private readonly ITaskService _taskService;

    public DeleteHandler(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<DeleteResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _taskService.DeleteAsync(request.Id, cancellationToken);

        if (!success)
            throw new KeyNotFoundException($"Task with ID {request.Id} not found");

        return new DeleteResponse { Success = true };
    }
}
