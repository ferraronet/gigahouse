using GigaHouse.Core.Common.Validation;
using MediatR;

namespace GigaHouse.Application.ProductMedias.Create;

public class CreateCommand : IRequest<CreateResult>
{
    public Guid ProductId { get; set; }

    public string Link { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;


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