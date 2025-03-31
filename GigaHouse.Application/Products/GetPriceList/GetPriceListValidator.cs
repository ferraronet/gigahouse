using FluentValidation;

namespace GigaHouse.Application.Products.GetPriceList;

public class GetPriceListValidator : AbstractValidator<GetPriceListCommand>
{
    public GetPriceListValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id cannot be empty.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product Id cannot be empty.");
    }
}
