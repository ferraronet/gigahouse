using FluentValidation;

namespace GigaHouse.Application.Tasks.GetList;

public class GetListRequestValidator : AbstractValidator<GetListRequest>
{
    public GetListRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
    }
}
