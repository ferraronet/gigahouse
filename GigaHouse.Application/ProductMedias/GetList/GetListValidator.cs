using FluentValidation;

namespace GigaHouse.Application.ProductMedias.GetList;

public class GetListValidator : AbstractValidator<GetListCommand>
{
    public GetListValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");
    }
}
