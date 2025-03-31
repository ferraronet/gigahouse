using GigaHouse.Core.Common.Validation;
using MediatR;

namespace GigaHouse.Application.Users.Create;

public class CreateCommand : IRequest<CreateResult>
{
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;


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