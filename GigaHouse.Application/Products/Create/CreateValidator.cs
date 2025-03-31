using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.Products.Create;

public class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(user => user.Name).NotEmpty().Length(3, 50);
        RuleFor(user => user.Gtin).NotEmpty().Length(3, 500);
    }
}