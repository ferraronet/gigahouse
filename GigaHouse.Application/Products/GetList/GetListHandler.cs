using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Core.Models;

namespace GigaHouse.Application.Products.GetList;

public class GetListHandler : IRequestHandler<GetListCommand, PaginatedList<GetListResult>>
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public GetListHandler(IMapper mapper, IProductService productService)
    {
        _mapper = mapper;
        _productService = productService;
    }

    public async Task<PaginatedList<GetListResult>> Handle(GetListCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetListValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedList = await _productService.GetAllProducts(request.PageNumber, request.PageSize, request.Gtin, request.Name, request.Status, cancellationToken);

        var pagedResponse = new PaginatedList<GetListResult>(_mapper.Map<List<GetListResult>>(paginatedList), paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);

        return pagedResponse;
    }
}
