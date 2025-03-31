using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Interfaces.Services;
using GigaHouse.Infrastructure.Services;

namespace GigaHouse.Application.ProjectProducts.Update;

public class UpdateHandler : IRequestHandler<UpdateCommand, UpdateResult>
{
    private readonly IMapper _mapper;
    private readonly IProjectProductService _projectProductService;

    public UpdateHandler(IMapper mapper, IProjectProductService projectProductService)
    {
        _mapper = mapper;
        _projectProductService = projectProductService;
    }

    public async Task<UpdateResult> Handle(UpdateCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingItem = await _projectProductService.GetByIdAsync(command.Id, cancellationToken);
        if (existingItem == null)
            throw new KeyNotFoundException($"Product with ID {command.Id} not exists");

        var updatedItem = await _projectProductService.UpdateAsync(_mapper.Map<ProjectProduct>(command), cancellationToken);
        var result = _mapper.Map<UpdateResult>(updatedItem);
        return result;
    }
}
