using FluentValidation;

namespace GigaHouse.Application.Users.Get;

public class GetValidator : AbstractValidator<GetCommand>
{
    public GetValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}
