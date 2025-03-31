using FluentValidation;

namespace GigaHouse.Application.UserProducts.Delete;

public class DeleteValidator : AbstractValidator<DeleteCommand>
{
    public DeleteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("userProduct ID is required");
    }
}
