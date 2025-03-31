using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.ProjectCssSelectors.Delete;

public class DeleteHandler : IRequestHandler<DeleteCommand, DeleteResponse>
{
    private readonly IProjectCssSelectorService _projectCssSelectorService;

    public DeleteHandler(IProjectCssSelectorService projectCssSelectorService)
    {
        _projectCssSelectorService = projectCssSelectorService;
    }

    public async Task<DeleteResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _projectCssSelectorService.DeleteAsync(request.Id, cancellationToken);

        if (!success)
            throw new KeyNotFoundException($"ProjectCssSelector with ID {request.Id} not found");

        return new DeleteResponse { Success = true };
    }
}
