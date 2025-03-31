using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.Delete;

public class DeleteRequestValidator : AbstractValidator<DeleteRequest>
{
    public DeleteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProjectCssSelector ID is required");
    }
}
