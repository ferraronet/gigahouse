using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.ProjectProducts.Get;

public class GetHandler : IRequestHandler<GetCommand, GetResult>
{
    private readonly IMapper _mapper;
    private readonly IProjectProductService _projectProductService;

    public GetHandler(IMapper mapper, IProjectProductService projectProductService)
    {
        _mapper = mapper;
        _projectProductService = projectProductService;
    }

    public async Task<GetResult> Handle(GetCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var item = await _projectProductService.GetByIdAsync(request.Id, cancellationToken);

        if (item == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        return _mapper.Map<GetResult>(item);
    }
}
