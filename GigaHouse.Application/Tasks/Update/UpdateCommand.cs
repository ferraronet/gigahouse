using GigaHouse.Core.Common.Validation;
using MediatR;

namespace GigaHouse.Application.Tasks.Update;

public class UpdateCommand : IRequest<UpdateResult>
{
    public Guid Id { get; set; }

    public string Link { get; set; } = string.Empty;

    public int TimesPerDay { get; set; }

    public Core.Enums.TaskStatus Status { get; set; }

    public Guid ProjectId { get; set; }

    public Guid ProductId { get; set; }


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