using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.Products.Update;

public class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(user => user.Name).NotEmpty().Length(3, 500);
        RuleFor(user => user.Gtin).NotEmpty().Length(3, 50);
        RuleFor(user => user.Status).NotEmpty();
    }
}