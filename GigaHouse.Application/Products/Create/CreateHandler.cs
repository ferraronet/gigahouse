using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.Products.Create;

public class CreateHandler : IRequestHandler<CreateCommand, CreateResult>
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public CreateHandler(IMapper mapper, IProductService productService)
    {
        _mapper = mapper;
        _productService = productService;
    }

    public async Task<CreateResult> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _productService.GetByGtinAsync(command.Gtin, cancellationToken);
        if (existingItem != null)
            throw new InvalidOperationException($"Product with Gtin {command.Gtin} already exists");

        var createdItem = await _productService.CreateAsync(_mapper.Map<Product>(command), cancellationToken);
        var result = _mapper.Map<CreateResult>(createdItem);
        return result;
    }
}
