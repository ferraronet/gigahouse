using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Core.Models;
using GigaHouse.Infrastructure.Services;

namespace GigaHouse.Application.ProjectProducts.GetList;

public class GetListHandler : IRequestHandler<GetListCommand, PaginatedList<GetListResult>>
{
    private readonly IMapper _mapper;
    private readonly IProjectProductService _projectProductService;

    public GetListHandler(IMapper mapper, IProjectProductService projectProductService)
    {
        _mapper = mapper;
        _projectProductService = projectProductService;
    }

    public async Task<PaginatedList<GetListResult>> Handle(GetListCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetListValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedList = await _projectProductService.GetAllByProjectIdAndProductId(request.ProjectId, request.ProductId, cancellationToken);

        var pagedResponse = new PaginatedList<GetListResult>(_mapper.Map<List<GetListResult>>(paginatedList), paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);

        return pagedResponse;
    }
}
