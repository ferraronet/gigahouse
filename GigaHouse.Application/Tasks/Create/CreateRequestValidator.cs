using FluentValidation;

namespace GigaHouse.Application.Tasks.Create;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(user => user.Link).NotEmpty();
        RuleFor(user => user.TimesPerDay).NotEmpty();
        RuleFor(user => user.ProjectId).NotEmpty();
        RuleFor(user => user.ProductId).NotEmpty();
    }
}