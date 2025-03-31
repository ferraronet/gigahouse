using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.ProjectProducts.Delete;

public class DeleteHandler : IRequestHandler<DeleteCommand, DeleteResponse>
{
    private readonly IProjectProductService _projectProductService;

    public DeleteHandler(IProjectProductService projectProductService)
    {
        _projectProductService = projectProductService;
    }

    public async Task<DeleteResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _projectProductService.DeleteAsync(request.Id, cancellationToken);

        if (!success)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        return new DeleteResponse { Success = true };
    }
}
