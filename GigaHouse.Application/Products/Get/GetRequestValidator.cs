using FluentValidation;

namespace GigaHouse.Application.Products.Get;

public class GetRequestValidator : AbstractValidator<GetRequest>
{
    public GetRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
