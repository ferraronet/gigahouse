using FluentValidation;

namespace GigaHouse.Application.ProjectProducts.Get;

public class GetRequestValidator : AbstractValidator<GetRequest>
{
    public GetRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProjectProduct ID is required");
    }
}
