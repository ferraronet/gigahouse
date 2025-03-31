using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.UserProducts.Create;

public class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(user => user.UserId).NotEmpty();
        RuleFor(user => user.ProductId).NotEmpty();
    }
}