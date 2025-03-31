using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.Projects.Update;

public class UpdateHandler : IRequestHandler<UpdateCommand, UpdateResult>
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public UpdateHandler(IMapper mapper, IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }

    public async Task<UpdateResult> Handle(UpdateCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _projectService.GetByIdAsync(command.Id, cancellationToken);
        if (existingItem == null)
            throw new KeyNotFoundException($"Project with name {command.Name} not exists");

        var updatedItem = await _projectService.UpdateAsync(_mapper.Map<Project>(command), cancellationToken);
        var result = _mapper.Map<UpdateResult>(updatedItem);
        return result;
    }
}
