using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.Tasks.Create;

public class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(user => user.Link).NotEmpty();
        RuleFor(user => user.TimesPerDay).NotEmpty();
        RuleFor(user => user.ProjectId).NotEmpty();
        RuleFor(user => user.ProductId).NotEmpty();
    }
}