using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Core.Models;

namespace GigaHouse.Application.ProductMedias.GetList;

public class GetListHandler : IRequestHandler<GetListCommand, PaginatedList<GetListResult>>
{
    private readonly IMapper _mapper;
    private readonly IProductMediaService _productMediaService;

    public GetListHandler(IMapper mapper, IProductMediaService productMediaService)
    {
        _mapper = mapper;
        _productMediaService = productMediaService;
    }

    public async Task<PaginatedList<GetListResult>> Handle(GetListCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetListValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedList = await _productMediaService.GetAllByProductId(request.ProductId, cancellationToken);

        var pagedResponse = new PaginatedList<GetListResult>(_mapper.Map<List<GetListResult>>(paginatedList), paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);

        return pagedResponse;
    }
}
