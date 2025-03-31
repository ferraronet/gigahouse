using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.Projects.Create;

public class CreateHandler : IRequestHandler<CreateCommand, CreateResult>
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public CreateHandler(IMapper mapper, IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }

    public async Task<CreateResult> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _projectService.GetByNameAsync(command.Name, cancellationToken);
        if (existingItem != null)
            throw new InvalidOperationException($"Project with name {command.Name} already exists");

        var createdItem = await _projectService.CreateAsync(_mapper.Map<Project>(command), cancellationToken);
        var result = _mapper.Map<CreateResult>(createdItem);
        return result;
    }
}
