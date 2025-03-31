using FluentValidation;

namespace GigaHouse.Application.Products.GetList;

public class GetListValidator : AbstractValidator<GetListCommand>
{
    public GetListValidator()
    {
        RuleFor(x => x.PageNumber)
             .GreaterThan(0).WithMessage("PageNumber must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than zero.");

        RuleFor(x => x.Gtin)
            .MaximumLength(100).WithMessage("Gtin cannot exceed 100 characters.");

        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}
