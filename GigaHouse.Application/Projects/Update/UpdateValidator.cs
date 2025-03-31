using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.Projects.Update;

public class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(user => user.Name).NotEmpty().Length(3, 50);
        RuleFor(user => user.Link).NotEmpty().Length(3, 500);
        RuleFor(user => user.Status).NotEmpty();
    }
}