using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.Users.Get;

public class GetHandler : IRequestHandler<GetCommand, GetResult>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<GetResult> Handle(GetCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await _userService.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {request.Id} not found");

        return _mapper.Map<GetResult>(user);
    }
}
