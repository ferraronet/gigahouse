using FluentValidation;

namespace GigaHouse.Application.Projects.Create;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(user => user.Name).NotEmpty().Length(3, 50);
        RuleFor(user => user.Link).NotEmpty().Length(3, 500);
    }
}