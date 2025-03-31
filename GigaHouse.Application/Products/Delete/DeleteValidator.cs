using FluentValidation;

namespace GigaHouse.Application.Products.Delete;

public class DeleteValidator : AbstractValidator<DeleteCommand>
{
    public DeleteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
