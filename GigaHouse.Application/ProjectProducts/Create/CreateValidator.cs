using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.ProjectProducts.Create;

public class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(user => user.ProjectId).NotEmpty();
        RuleFor(user => user.ProductId).NotEmpty();
    }
}