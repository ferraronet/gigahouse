using FluentValidation;

namespace GigaHouse.Application.Products.GetPriceList;

public class GetPriceListRequestValidator : AbstractValidator<GetPriceListRequest>
{
    public GetPriceListRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id cannot be empty.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product Id cannot be empty.");
    }
}
