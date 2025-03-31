using FluentValidation;

namespace GigaHouse.Application.UserProducts.Get;

public class GetRequestValidator : AbstractValidator<GetRequest>
{
    public GetRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("UserProduct ID is required");
    }
}
