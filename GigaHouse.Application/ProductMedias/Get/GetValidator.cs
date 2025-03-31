using FluentValidation;

namespace GigaHouse.Application.ProductMedias.Get;

public class GetValidator : AbstractValidator<GetCommand>
{
    public GetValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Media ID is required");
    }
}
