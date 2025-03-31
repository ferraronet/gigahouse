using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.Products.GetPriceList;

public class GePricetListHandler : IRequestHandler<GetPriceListCommand, GetPriceListResult>
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public GePricetListHandler(IMapper mapper, IProductService productService)
    {
        _mapper = mapper;
        _productService = productService;
    }

    public async Task<GetPriceListResult> Handle(GetPriceListCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetPriceListValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var item = await _productService.GetProductPricesByUserIdAsync(request.UserId, request.ProductId, cancellationToken);

        if (item == null)
            throw new KeyNotFoundException($"Product with ID {request.ProductId} not found");

        return _mapper.Map<GetPriceListResult>(item);
    }
}
