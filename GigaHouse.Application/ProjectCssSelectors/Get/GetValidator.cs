using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.Get;

public class GetValidator : AbstractValidator<GetCommand>
{
    public GetValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProjectCssSelector ID is required");
    }
}
