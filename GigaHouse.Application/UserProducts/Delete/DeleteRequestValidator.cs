using FluentValidation;

namespace GigaHouse.Application.UserProducts.Delete;

public class DeleteRequestValidator : AbstractValidator<DeleteRequest>
{
    public DeleteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("UserProduct ID is required");
    }
}
