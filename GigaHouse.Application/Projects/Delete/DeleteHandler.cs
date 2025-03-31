using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.Projects.Delete;

public class DeleteHandler : IRequestHandler<DeleteCommand, DeleteResponse>
{
    private readonly IProjectService _projectService;

    public DeleteHandler(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<DeleteResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _projectService.DeleteAsync(request.Id, cancellationToken);

        if (!success)
            throw new KeyNotFoundException($"Project with ID {request.Id} not found");

        return new DeleteResponse { Success = true };
    }
}
