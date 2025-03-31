using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;
using AutoMapper;
using GigaHouse.Infrastructure.Services;

namespace GigaHouse.Application.UserProducts.Delete;

public class DeleteHandler : IRequestHandler<DeleteCommand, DeleteResponse>
{
    private readonly IUserProductService _userProductService;

    public DeleteHandler(IUserProductService userProductService)
    {
        _userProductService = userProductService;
    }

    public async Task<DeleteResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _userProductService.DeleteAsync(request.Id, cancellationToken);

        if (!success)
            throw new KeyNotFoundException($"UserProduct with ID {request.Id} not found");

        return new DeleteResponse { Success = true };
    }
}
