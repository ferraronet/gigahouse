using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.ProductMedias.Get;

public class GetHandler : IRequestHandler<GetCommand, GetResult>
{
    private readonly IMapper _mapper;
    private readonly IProductMediaService _productMediaService;

    public GetHandler(IMapper mapper, IProductMediaService productMediaService)
    {
        _mapper = mapper;
        _productMediaService = productMediaService;
    }

    public async Task<GetResult> Handle(GetCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var item = await _productMediaService.GetByIdAsync(request.Id, cancellationToken);

        if (item == null)
            throw new KeyNotFoundException($"Media with ID {request.Id} not found");

        return _mapper.Map<GetResult>(item);
    }
}
