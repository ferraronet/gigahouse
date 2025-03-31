using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Services;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Core.Models;

namespace GigaHouse.Application.Projects.GetList;

public class GetListHandler : IRequestHandler<GetListCommand, PaginatedList<GetListResult>>
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public GetListHandler(IMapper mapper, IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }

    public async Task<PaginatedList<GetListResult>> Handle(GetListCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetListValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var paginatedList = await _projectService.GetAllProjects(request.PageNumber, request.PageSize, request.Name, request.Status, cancellationToken);

        var pagedResponse = new PaginatedList<GetListResult>(_mapper.Map<List<GetListResult>>(paginatedList), paginatedList.TotalCount, paginatedList.CurrentPage, paginatedList.PageSize);

        return pagedResponse;
    }
}
