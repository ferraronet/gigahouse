using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.Create;

public class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(user => user.ProjectId).NotEmpty();
    }
}