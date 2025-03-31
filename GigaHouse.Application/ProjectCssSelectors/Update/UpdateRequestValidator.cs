using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.Update;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ProjectCssSelector ID is required");
    }
}