using GigaHouse.Core.Enums;

namespace GigaHouse.Application.Users.Get;

public class GetResult
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;   

    public UserStatus Status { get; set; }
}
