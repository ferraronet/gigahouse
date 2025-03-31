using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.Products.Update;

public class UpdateHandler : IRequestHandler<UpdateCommand, UpdateResult>
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public UpdateHandler(IMapper mapper, IProductService productService)
    {
        _mapper = mapper;
        _productService = productService;
    }

    public async Task<UpdateResult> Handle(UpdateCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _productService.GetByIdAsync(command.Id, cancellationToken);
        if (existingItem == null)
            throw new KeyNotFoundException($"Product with name {command.Name} not exists");

        var updatedItem = await _productService.UpdateAsync(_mapper.Map<Product>(command), cancellationToken);
        var result = _mapper.Map<UpdateResult>(updatedItem);
        return result;
    }
}
