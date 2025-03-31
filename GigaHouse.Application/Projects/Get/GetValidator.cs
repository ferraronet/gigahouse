using FluentValidation;

namespace GigaHouse.Application.Projects.Get;

public class GetValidator : AbstractValidator<GetCommand>
{
    public GetValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Project ID is required");
    }
}
