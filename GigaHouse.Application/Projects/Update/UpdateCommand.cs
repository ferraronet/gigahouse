using GigaHouse.Core.Common.Validation;
using GigaHouse.Core.Enums;
using MediatR;

namespace GigaHouse.Application.Projects.Update;

public class UpdateCommand : IRequest<UpdateResult>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;

    public ProjectStatus Status { get; set; }


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