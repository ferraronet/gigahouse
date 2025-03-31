using FluentValidation;

namespace GigaHouse.Application.ProjectProducts.Update;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ProjectProduct ID is required");
    }
}