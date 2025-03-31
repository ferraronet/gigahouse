using FluentValidation;

namespace GigaHouse.Application.ProjectProducts.Get;

public class GetValidator : AbstractValidator<GetCommand>
{
    public GetValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProjectProduct ID is required");
    }
}
