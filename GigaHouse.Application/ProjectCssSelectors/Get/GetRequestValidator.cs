using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.Get;

public class GetRequestValidator : AbstractValidator<GetRequest>
{
    public GetRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProjectCssSelector ID is required");
    }
}
