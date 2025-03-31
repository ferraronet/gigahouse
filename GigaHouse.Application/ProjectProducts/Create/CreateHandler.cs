using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.ProjectProducts.Create;

public class CreateHandler : IRequestHandler<CreateCommand, CreateResult>
{
    private readonly IMapper _mapper;
    private readonly IProjectProductService _projectProductService;

    public CreateHandler(IMapper mapper, IProjectProductService projectProductService)
    {
        _mapper = mapper;
        _projectProductService = projectProductService;
    }

    public async Task<CreateResult> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _projectProductService.GetByProjectIdAndProductIdAsync(command.ProjectId, command.ProductId, cancellationToken);
        if (existingItem != null)
            throw new InvalidOperationException($"Product with ID {command.ProductId} already exists");

        var createdItem = await _projectProductService.CreateAsync(_mapper.Map<ProjectProduct>(command), cancellationToken);
        var result = _mapper.Map<CreateResult>(createdItem);
        return result;
    }
}
