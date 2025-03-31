using FluentValidation;

namespace GigaHouse.Application.ProjectProducts.Delete;

public class DeleteRequestValidator : AbstractValidator<DeleteRequest>
{
    public DeleteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProjectProduct ID is required");
    }
}
