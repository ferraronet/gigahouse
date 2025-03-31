using GigaHouse.Core.Common.Validation;
using MediatR;

namespace GigaHouse.Application.Projects.Create;

public class CreateCommand : IRequest<CreateResult>
{
    public string Name { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;


    public ValidationResultDetail Validate()
    {
        var validator = new CreateValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}