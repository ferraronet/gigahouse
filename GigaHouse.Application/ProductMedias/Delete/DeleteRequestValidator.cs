using FluentValidation;

namespace GigaHouse.Application.ProductMedias.Delete;

public class DeleteRequestValidator : AbstractValidator<DeleteRequest>
{
    public DeleteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Media ID is required");
    }
}
