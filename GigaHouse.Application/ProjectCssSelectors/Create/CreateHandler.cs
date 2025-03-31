using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.ProjectCssSelectors.Create;

public class CreateHandler : IRequestHandler<CreateCommand, CreateResult>
{
    private readonly IMapper _mapper;
    private readonly IProjectCssSelectorService _projectCssSelectorService;

    public CreateHandler(IMapper mapper, IProjectCssSelectorService projectCssSelectorService)
    {
        _mapper = mapper;
        _projectCssSelectorService = projectCssSelectorService;
    }

    public async Task<CreateResult> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var createdItem = await _projectCssSelectorService.CreateAsync(_mapper.Map<ProjectCssSelector>(command), cancellationToken);
        var result = _mapper.Map<CreateResult>(createdItem);
        return result;
    }
}
