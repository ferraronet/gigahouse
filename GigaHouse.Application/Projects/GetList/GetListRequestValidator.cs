using FluentValidation;

namespace GigaHouse.Application.Projects.GetList;

public class GetListRequestValidator : AbstractValidator<GetListRequest>
{
    public GetListRequestValidator()
    {
        RuleFor(x => x.PageNumber)
             .GreaterThan(0).WithMessage("PageNumber must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than zero.");

        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}
