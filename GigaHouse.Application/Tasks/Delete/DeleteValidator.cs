using FluentValidation;

namespace GigaHouse.Application.Tasks.Delete;

public class DeleteValidator : AbstractValidator<DeleteCommand>
{
    public DeleteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Task ID is required");
    }
}
