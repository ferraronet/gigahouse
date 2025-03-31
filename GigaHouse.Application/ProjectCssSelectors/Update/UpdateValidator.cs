using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.Update;

public class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ProjectCssSelector ID is required");
    }
}