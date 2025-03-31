using FluentValidation;

namespace GigaHouse.Application.UserProducts.Create;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(user => user.UserId).NotEmpty();
        RuleFor(user => user.ProductId).NotEmpty();
    }
}