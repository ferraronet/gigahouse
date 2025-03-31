using FluentValidation;

namespace GigaHouse.Application.Products.Update;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(user => user.Name).NotEmpty().Length(3, 500);
        RuleFor(user => user.Gtin).NotEmpty().Length(3, 50);
        RuleFor(user => user.Status).NotEmpty();
    }
}