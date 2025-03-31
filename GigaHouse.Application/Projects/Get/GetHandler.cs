using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.Projects.Get;

public class GetHandler : IRequestHandler<GetCommand, GetResult>
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public GetHandler(IMapper mapper, IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }

    public async Task<GetResult> Handle(GetCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var item = await _projectService.GetByIdAsync(request.Id, cancellationToken);

        if (item == null)
            throw new KeyNotFoundException($"Project with ID {request.Id} not found");

        return _mapper.Map<GetResult>(item);
    }
}
