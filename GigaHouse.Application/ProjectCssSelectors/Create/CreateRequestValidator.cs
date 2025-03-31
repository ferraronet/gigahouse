using FluentValidation;

namespace GigaHouse.Application.ProjectCssSelectors.Create;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(user => user.ProjectId).NotEmpty();
    }
}