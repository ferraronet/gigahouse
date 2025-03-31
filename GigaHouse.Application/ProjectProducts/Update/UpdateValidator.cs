using GigaHouse.Data.Validation;
using FluentValidation;

namespace GigaHouse.Application.ProjectProducts.Update;

public class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ProjectProduct ID is required");
    }
}