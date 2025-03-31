using FluentValidation;

namespace GigaHouse.Application.Tasks.Update;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Task ID is required");
        RuleFor(user => user.Link).NotEmpty().WithMessage("Link is required").Length(3, 500).WithMessage("Length need be between 3 and 500");
        RuleFor(user => user.TimesPerDay).NotEmpty().WithMessage("TimesPerDay is required");
        RuleFor(user => user.Status).NotEmpty().WithMessage("Status is required");
    }
}