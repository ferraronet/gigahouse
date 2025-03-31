using FluentValidation;

namespace GigaHouse.Application.ProductMedias.Delete;

public class DeleteValidator : AbstractValidator<DeleteCommand>
{
    public DeleteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Media ID is required");
    }
}
