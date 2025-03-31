using FluentValidation;

namespace GigaHouse.Application.ProductMedias.Create;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(user => user.ProductId).NotEmpty();
        RuleFor(user => user.Link).NotEmpty().Length(3, 500);
        RuleFor(user => user.Type).NotEmpty().Length(3, 50);
    }
}