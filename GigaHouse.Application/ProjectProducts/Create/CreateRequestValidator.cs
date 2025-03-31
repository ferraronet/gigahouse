using FluentValidation;

namespace GigaHouse.Application.ProjectProducts.Create;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(user => user.ProjectId).NotEmpty();
        RuleFor(user => user.ProductId).NotEmpty();
    }
}