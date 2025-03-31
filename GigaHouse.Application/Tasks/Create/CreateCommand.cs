using GigaHouse.Core.Common.Validation;
using MediatR;

namespace GigaHouse.Application.Tasks.Create;

public class CreateCommand : IRequest<CreateResult>
{
    public string Link { get; set; } = string.Empty;

    public int TimesPerDay { get; set; }

    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }


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