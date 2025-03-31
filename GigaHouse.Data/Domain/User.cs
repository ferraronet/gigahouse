using GigaHouse.Core.Enums;
using GigaHouse.Data.Common;
using GigaHouse.Core.Common.Security;

namespace GigaHouse.Data.Domain
{
    public class User : BaseEntity, IUser
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserStatus Status { get; set; }

        string IUser.Id => Id.ToString();

        string IUser.Username => Username;

        public User()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            Status = UserStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            Status = UserStatus.Inactive;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Suspend()
        {
            Status = UserStatus.Suspended;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
