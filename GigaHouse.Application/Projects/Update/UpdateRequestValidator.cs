using FluentValidation;

namespace GigaHouse.Application.Projects.Update;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(user => user.Name).NotEmpty().Length(3, 50);
        RuleFor(user => user.Link).NotEmpty().Length(3, 500);
        RuleFor(user => user.Status).NotEmpty();
    }
}