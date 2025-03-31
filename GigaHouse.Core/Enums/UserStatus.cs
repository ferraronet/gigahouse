using System.ComponentModel.DataAnnotations;

namespace GigaHouse.Core.Enums
{
    public enum UserStatus
    {
        [Display(Name = "Pending")]
        Pending = 0,

        [Display(Name = "Active")]
        Active = 1,

        [Display(Name = "Inactive")]
        Inactive = 2,

        [Display(Name = "Suspended")]
        Suspended = 3
    }
}
