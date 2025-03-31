using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.GetList;

public class GetListRequestValidator : AbstractValidator<GetListRequest>
{
    public GetListRequestValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("ProjectId ID is required");
    }
}
