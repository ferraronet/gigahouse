using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.Delete;

public class DeleteValidator : AbstractValidator<DeleteCommand>
{
    public DeleteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProjectCssSelector ID is required");
    }
}
