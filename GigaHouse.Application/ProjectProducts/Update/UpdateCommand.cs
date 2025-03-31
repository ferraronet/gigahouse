using GigaHouse.Core.Common.Validation;
using GigaHouse.Core.Enums;
using MediatR;

namespace GigaHouse.Application.ProjectProducts.Update;

public class UpdateCommand : IRequest<UpdateResult>
{
    public Guid Id { get; set; }

    public string MetaKeywords { get; set; } = string.Empty;

    public string MetaTitle { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;


    public ValidationResultDetail Validate()
    {
        var validator = new UpdateValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}