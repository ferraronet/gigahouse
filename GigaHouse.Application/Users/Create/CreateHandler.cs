using AutoMapper;
using MediatR;
using FluentValidation;
using GigaHouse.Data.Domain;
using GigaHouse.Core.Common.Security;
using GigaHouse.Infrastructure.Interfaces.Services;

namespace GigaHouse.Application.Users.Create;

public class CreateHandler : IRequestHandler<CreateCommand, CreateResult>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;


    public CreateHandler(IUserService userService, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _userService = userService;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<CreateResult> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await _userService.GetByEmailAsync(command.Email, cancellationToken);
        if (existingUser != null)
            throw new InvalidOperationException($"User with email {command.Email} already exists");

        var user = _mapper.Map<User>(command);
        user.Password = _passwordHasher.HashPassword(command.Password);

        var createdUser = await _userService.CreateAsync(user, cancellationToken);
        var result = _mapper.Map<CreateResult>(createdUser);
        return result;
    }
}
