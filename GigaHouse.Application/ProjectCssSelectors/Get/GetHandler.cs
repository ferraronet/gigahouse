using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.ProjectCssSelectors.Get;

public class GetHandler : IRequestHandler<GetCommand, GetResult>
{
    private readonly IMapper _mapper;
    private readonly IProjectCssSelectorService _projectCssSelectorService;

    public GetHandler(IMapper mapper, IProjectCssSelectorService projectCssSelectorService)
    {
        _mapper = mapper;
        _projectCssSelectorService = projectCssSelectorService;
    }

    public async Task<GetResult> Handle(GetCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var item = await _projectCssSelectorService.GetByIdAsync(request.Id, cancellationToken);

        if (item == null)
            throw new KeyNotFoundException($"ProjectCssSelector with ID {request.Id} not found");

        return _mapper.Map<GetResult>(item);
    }
}
