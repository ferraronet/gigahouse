using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Services;

namespace GigaHouse.Application.ProjectCssSelectors.Update;

public class UpdateHandler : IRequestHandler<UpdateCommand, UpdateResult>
{
    private readonly IMapper _mapper;
    private readonly IProjectCssSelectorService _projectCssSelectorService;

    public UpdateHandler(IMapper mapper, IProjectCssSelectorService projectCssSelectorService)
    {
        _mapper = mapper;
        _projectCssSelectorService = projectCssSelectorService;
    }

    public async Task<UpdateResult> Handle(UpdateCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _projectCssSelectorService.GetByIdAsync(command.Id, cancellationToken);
        if (existingItem == null)
            throw new KeyNotFoundException($"ProjectCssSelector with ID {command.Id} not exists");

        var updatedItem = await _projectCssSelectorService.UpdateAsync(_mapper.Map<ProjectCssSelector>(command), cancellationToken);
        var result = _mapper.Map<UpdateResult>(updatedItem);
        return result;
    }
}
