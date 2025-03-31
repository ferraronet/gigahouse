using FluentValidation;

namespace GigaHouse.Application.Products.Create;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(user => user.Name).NotEmpty().Length(3, 500);
        RuleFor(user => user.Gtin).NotEmpty().Length(3, 50);
    }
}