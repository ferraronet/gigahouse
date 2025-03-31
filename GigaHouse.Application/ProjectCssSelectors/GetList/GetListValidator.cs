using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.GetList;

public class GetListValidator : AbstractValidator<GetListCommand>
{
    public GetListValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("ProjectId ID is required");
    }
}
