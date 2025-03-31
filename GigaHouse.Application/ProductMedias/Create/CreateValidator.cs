using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.ProductMedias.Create;

public class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(user => user.ProductId).NotEmpty();
        RuleFor(user => user.Link).NotEmpty().Length(3, 500);
        RuleFor(user => user.Type).NotEmpty().Length(3, 50);
    }
}