using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Core.Models;
using GigaHouse.Infrastructure.Services;

namespace GigaHouse.Application.ProjectCssSelectors.GetList;

public class GetListHandler : IRequestHandler<GetListCommand, PaginatedList<GetListResult>>
{
    private readonly IMapper _mapper;
    private readonly IProjectCssSelectorService _projectCssSelectorService;

    public GetListHandler(IMapper mapper, IProjectCssSelectorService projectCssSelectorService)
    {
        _mapper = mapper;
        _projectCssSelectorService = projectCssSelectorService;
    }

    public async Task<PaginatedList<GetListResult>> Handle(GetListCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetListValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedList = await _projectCssSelectorService.GetAllByProjectIdAsync(request.ProjectId, cancellationToken);

        var pagedResponse = new PaginatedList<GetListResult>(_mapper.Map<List<GetListResult>>(paginatedList), paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);

        return pagedResponse;
    }
}
