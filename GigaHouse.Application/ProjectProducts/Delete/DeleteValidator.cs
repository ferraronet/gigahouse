using FluentValidation;

namespace GigaHouse.Application.ProjectProducts.Delete;

public class DeleteValidator : AbstractValidator<DeleteCommand>
{
    public DeleteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProjectProduct ID is required");
    }
}
