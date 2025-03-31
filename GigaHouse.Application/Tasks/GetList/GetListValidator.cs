using FluentValidation;

namespace GigaHouse.Application.Tasks.GetList;

public class GetListValidator : AbstractValidator<GetListCommand>
{
    public GetListValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
    }
}
