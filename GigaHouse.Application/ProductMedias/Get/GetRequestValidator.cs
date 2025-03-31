using FluentValidation;

namespace GigaHouse.Application.ProductMedias.Get;

public class GetRequestValidator : AbstractValidator<GetRequest>
{
    public GetRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Media ID is required");
    }
}
